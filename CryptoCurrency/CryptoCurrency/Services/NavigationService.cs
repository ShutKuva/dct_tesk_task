using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoCurrency.Abstractions.Services;
using CryptoCurrency.ViewModels.BaseClasses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoCurrency.Services
{
    public class NavigationService : ObservableObject, INavigationService<BaseViewModel>
    {
        private int _currentPositionInList = -1;
        private readonly List<BaseViewModel> _viewModelList = new();
        private readonly ICommand _nextCommand;
        private readonly ICommand _previousCommand;

        public NavigationService()
        {
            _nextCommand = new RelayCommand(Next);
            _previousCommand = new RelayCommand(Previous);
        }

        public BaseViewModel CurrentViewModel 
        {
            get
            {
                if (_currentPositionInList == -1)
                {
                    return null!;
                }

                return _viewModelList[_currentPositionInList];
            }
            set 
            {
                _viewModelList.RemoveRange(_currentPositionInList + 1, _viewModelList.Count - _currentPositionInList - 1);

                ++_currentPositionInList;
                _viewModelList.Add(value);

                OnPropertyChanged();
                OnPropertyChanged(nameof(IsPreviousViewModel));
                OnPropertyChanged(nameof(IsNextViewModel));
            }
        }

        public bool IsPreviousViewModel => _viewModelList.Count > 1 && _currentPositionInList != 0;
        public bool IsNextViewModel => _currentPositionInList + 1 < _viewModelList.Count;

        public ICommand PreviousViewModel => _previousCommand;
        public ICommand NextViewModel => _nextCommand;

        private void Next()
        {
            if (IsNextViewModel)
            {
                _currentPositionInList++;
                OnPropertyChanged(nameof(CurrentViewModel));
                OnPropertyChanged(nameof(IsPreviousViewModel));
                OnPropertyChanged(nameof(IsNextViewModel));
            }
        }

        private void Previous()
        {
            if (IsPreviousViewModel)
            {
                _currentPositionInList--;
                OnPropertyChanged(nameof(CurrentViewModel));
                OnPropertyChanged(nameof(IsPreviousViewModel));
                OnPropertyChanged(nameof(IsNextViewModel));
            }
        }

        public Task StartAsync(BaseViewModel viewModel)
        {
            CurrentViewModel = viewModel;
            return Task.CompletedTask;
        }
    }
}
