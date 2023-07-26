using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoCurrency.Abstractions.Adapters;
using CryptoCurrency.Models;
using CryptoCurrency.ViewModels.BaseClasses;
using LiveCharts.Defaults;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CryptoCurrency.ViewModels
{
    public partial class DetailsViewModel : BaseViewModel
    {
        private readonly IHistoryReceiverAdapter<ObservablePoint> _historyReceiverAdapter;

        [ObservableProperty]
        private IEnumerable<ObservablePoint> _points = default!;

        [ObservableProperty]
        private string _currentPeriod = "h1";

        public DetailsViewModel(IHistoryReceiverAdapter<ObservablePoint> historyReceiverAdapter)
        {
            _historyReceiverAdapter = historyReceiverAdapter;
        }

        public DetailedCurrency Currency { get; set; } = default!;

        public List<string> Periods { get; set; } = new List<string>()
        {
            "m1", "m5", "m15", "m30", "h1", "h2", "h6", "h12", "d1"
        };

        [RelayCommand]
        private async Task Start()
        {
            await PeriodChanged(CurrentPeriod);
        }

        [RelayCommand]
        private async Task PeriodChanged(string period)
        {
            Points = await _historyReceiverAdapter.GetSeries(Currency.Id, period, null, null);
            CurrentPeriod = period;
        }
    }
}
