using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using SampleProject.API.InternalCommands;
using SampleProject.API.Modules;
using SampleProject.API.Outbox;
using SampleProject.Infrastructure;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.API
{
    /// <summary>
    /// 
    /// </summary>
    public class AutofacServiceProviderFactory : IServiceProviderFactory<ContainerBuilder>
    {
        private readonly Action<ContainerBuilder> _configurationAction;

        private readonly IConfiguration _configuration;


        private const string OrdersConnectionString = nameof(OrdersConnectionString);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationAction"></param>
        public AutofacServiceProviderFactory(IConfiguration configuration, Action<ContainerBuilder> configurationAction = null)
        {
            _configuration = configuration;
            _configurationAction = configurationAction ?? (builder => { });
        }

        public ContainerBuilder CreateBuilder(IServiceCollection services)
        {
            var container = new ContainerBuilder();


            container.Populate(services);
            _configurationAction(container);

            return container;
        }

        public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        {
            if (containerBuilder == null) throw new ArgumentNullException(nameof(containerBuilder));


            containerBuilder.RegisterModule(new InfrastructureModule(_configuration[OrdersConnectionString]));

            containerBuilder.RegisterModule(new MediatorModule());

            containerBuilder.RegisterModule(new ForeignExchangeModule());

            containerBuilder.RegisterModule(new DomainModule());

            containerBuilder.RegisterModule(new EmailModule());

            var children = this._configuration.GetSection("Caching").GetChildren();
            Dictionary<string, TimeSpan> configuration = children.ToDictionary(child => child.Key, child => TimeSpan.Parse(child.Value));
            containerBuilder.RegisterModule(new CachingModule(configuration));

            var container = containerBuilder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));

            return new AutofacServiceProvider(container);
        }
    }
}
