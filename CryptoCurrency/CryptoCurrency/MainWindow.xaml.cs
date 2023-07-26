using CryptoCurrency.ViewModels;
using System.Windows;

namespace CryptoCurrency
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
