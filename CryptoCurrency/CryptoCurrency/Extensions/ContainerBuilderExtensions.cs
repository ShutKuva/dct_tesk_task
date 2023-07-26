using Autofac;
using AutoMapper;
using BLL;
using Core;
using CryptoCurrency.Abstractions.Adapters;
using CryptoCurrency.Abstractions.Services;
using CryptoCurrency.Adapters;
using CryptoCurrency.AutoMapper;
using CryptoCurrency.Models;
using CryptoCurrency.Services;
using CryptoCurrency.ViewModels;
using CryptoCurrency.ViewModels.BaseClasses;
using LiveCharts.Defaults;
using Microsoft.Extensions.Configuration;
using System;

namespace CryptoCurrency.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder ConfigureDependencies(this ContainerBuilder containerBuilder)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");

            IConfigurationRoot configurations = configurationBuilder.Build();

            containerBuilder.RegisterType<DetailsViewModel>();
            containerBuilder.RegisterType<HomeViewModel>();
            containerBuilder.RegisterType<MainViewModel>();
            containerBuilder.RegisterType<MainWindow>();

            containerBuilder.RegisterType<EntityReceiverAdapter>().As<IEntityReceiverAdapter<BriefCurrency, DetailedCurrency>>();
            containerBuilder.RegisterType<NavigationService>().As<INavigationService<BaseViewModel>>().SingleInstance();
            containerBuilder.RegisterType<ThemeService>().As<IThemeService<string, string>>();
            containerBuilder.RegisterType<HistoryReceiverAdapter>().As<IHistoryReceiverAdapter<ObservablePoint>>();

            containerBuilder
                .Register<Func<Type, BaseViewModel>>(context =>
                {
                    IComponentContext resolvedContext = context.Resolve<IComponentContext>();
                    return viewModelType => (BaseViewModel)resolvedContext.Resolve(viewModelType);
                });

            containerBuilder.RegisterInstance(configurations.GetSection("ApiConfigurations").Get<ApiConfigurations>());
            containerBuilder.RegisterInstance(configurations.GetSection("BaseConfigurations").Get<BaseConfigurations>());
            containerBuilder.RegisterInstance(configurations.GetSection("Themes").Get<Themes>());

            containerBuilder.AddAutoMapper();

            DependencyRegistrator.RegisterDependencies(containerBuilder, configurations);

            return containerBuilder;
        }

        public static ContainerBuilder AddAutoMapper(this ContainerBuilder containerBuilder)
        {
            var configurations = new MapperConfiguration(exp =>
            {
                exp.AddProfile<CurrencyProfile>();
            });

            containerBuilder.RegisterInstance(configurations.CreateMapper());

            return containerBuilder;
        }
    }
}
