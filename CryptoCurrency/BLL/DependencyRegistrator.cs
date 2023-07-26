using Autofac;
using BLL.Abstractions;
using BLL.Models;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public static class DependencyRegistrator
    {
        public static void RegisterDependencies(ContainerBuilder containerBuilder, IConfiguration configurations)
        {
            containerBuilder.RegisterType<HttpService>().As<IHttpService>();
            containerBuilder.RegisterType<EntityReceiver>().As<IEntityReceiver<BriefCurrency, DetailedCurrency>>();
            containerBuilder.RegisterType<HistoryReceiver>().As<IHistoryReceiver<HistoryNode>>();
        }
    }
}
