using System.Windows.Input;

namespace CryptoCurrency.Abstractions.Services
{
    public interface INavigationService<TViewModel> : IStartableService<TViewModel>
    {
        public TViewModel CurrentViewModel { get; set; }

        public bool IsPreviousViewModel { get; }
        public bool IsNextViewModel { get; }

        public ICommand PreviousViewModel { get; }
        public ICommand NextViewModel { get; }
    }
}
