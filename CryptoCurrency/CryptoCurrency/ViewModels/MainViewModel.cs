using CommunityToolkit.Mvvm.Input;
using CryptoCurrency.Abstractions.Services;
using CryptoCurrency.ViewModels.BaseClasses;
using System;
using System.Windows.Input;

namespace CryptoCurrency.ViewModels
{
    public class MainViewModel
    {
        private readonly Func<Type, BaseViewModel> _viewModelFabric;

        public MainViewModel(
            INavigationService<BaseViewModel> navigationService,
            Func<Type, BaseViewModel> viewModelFabric,
            IThemeService<string, string> themeService)
        {
            StartCommand = new RelayCommand(Start);

            NavigationService = navigationService;
            _viewModelFabric = viewModelFabric;
            ThemeService = themeService;
        }

        public ICommand StartCommand { get; }

        public INavigationService<BaseViewModel> NavigationService { get; }
        public IThemeService<string, string> ThemeService { get; }

        private void Start()
        {
            NavigationService.StartAsync(_viewModelFabric(typeof(HomeViewModel)));
            ThemeService.StartAsync(null!);
        }
    }
}
