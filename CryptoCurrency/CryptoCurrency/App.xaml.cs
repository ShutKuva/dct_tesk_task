using Autofac;
using CryptoCurrency.Extensions;
using System.Windows;

namespace CryptoCurrency
{
    public partial class App : Application
    {
        private readonly IContainer _container;

        public App()
        {
            _container = new ContainerBuilder().ConfigureDependencies().Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _container.Resolve<MainWindow>().Show();
        }
    }
}
