using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core;
using CryptoCurrency.Abstractions.Adapters;
using CryptoCurrency.Abstractions.Services;
using CryptoCurrency.Models;
using CryptoCurrency.ViewModels.BaseClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CryptoCurrency.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly IEntityReceiverAdapter<BriefCurrency, DetailedCurrency> _entityReceiverAdapter;
        private readonly BaseConfigurations _baseConfigurations;
        private readonly INavigationService<BaseViewModel> _navigationService;
        private readonly Func<Type, BaseViewModel> _viewModelFabric;

        [ObservableProperty]
        private IEnumerable<BriefCurrency> _currencies = new List<BriefCurrency>();

        [ObservableProperty]
        private string _search = string.Empty;

        [ObservableProperty]
        private string _numberOfEntities = string.Empty;

        private bool _isLoading = false;

        public HomeViewModel(
            IEntityReceiverAdapter<BriefCurrency, DetailedCurrency> entityReceiverAdapter,
            BaseConfigurations baseConfigurations, 
            INavigationService<BaseViewModel> navigationService,
            Func<Type, BaseViewModel> viewModelFabric)
        {
            GoToDetailsCommand = new AsyncRelayCommand<string>(GoToDetails);
            SearchingCommand = new AsyncRelayCommand(Searching);

            _entityReceiverAdapter = entityReceiverAdapter;
            _baseConfigurations = baseConfigurations;
            _navigationService = navigationService;
            _viewModelFabric = viewModelFabric;
        }

        public IAsyncRelayCommand GoToDetailsCommand { get; }
        public IAsyncRelayCommand SearchingCommand { get; }

        public Visibility LoadingVisibility => _isLoading ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ListBoxVisibility => _isLoading ? Visibility.Collapsed : Visibility.Visible;

        private async Task Searching()
        {
            SetLoading(true);
            Currencies = await _entityReceiverAdapter.GetTopEntities(0, int.TryParse(NumberOfEntities, out int numberOfEntities) ? numberOfEntities : _baseConfigurations.DefaultCountOfCurrenciesOnHomePage, Search);
            SetLoading(false);
        }
        
        private async Task GoToDetails(string? id)
        {
            if (id is null)
            {
                return;
            }

            DetailedCurrency? detailedCurrency = await _entityReceiverAdapter.GetDetailedEntity(id);
            if (detailedCurrency is null)
            {
                return;
            }

            DetailsViewModel? detailsViewModel = _viewModelFabric(typeof(DetailsViewModel)) as DetailsViewModel;
            if (detailsViewModel is null)
            {
                return;
            }

            detailsViewModel.Currency = detailedCurrency;

            _navigationService.CurrentViewModel = detailsViewModel;
        }

        private void SetLoading(bool isLoading)
        {
            _isLoading = isLoading;
            OnPropertyChanged(nameof(LoadingVisibility));
            OnPropertyChanged(nameof(ListBoxVisibility));
        }
    }
}
