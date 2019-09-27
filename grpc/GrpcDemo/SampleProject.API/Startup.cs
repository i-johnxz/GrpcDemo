using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using SampleProject.API.InternalCommands;
using SampleProject.API.Modules;
using SampleProject.API.Outbox;
using SampleProject.API.SeedWork;
using SampleProject.Domain.SeedWork;
using SampleProject.Infrastructure;
using SampleProject.Infrastructure.SeedWork;
using Swashbuckle.AspNetCore.Swagger;

namespace SampleProject.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private const string OrdersConnectionString = nameof(OrdersConnectionString);

        private ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            
            AddSwagger(services);

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<OrdersContext>(options =>
                {
                    options.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>()
                        .UseSqlServer(this._configuration[OrdersConnectionString]);
                });


            services.AddProblemDetails(x =>
            {
                x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
            });

            return CreateAutofacServiceProvider(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseProblemDetails();
            }


            this.StartQuartz(serviceProvider);
            
            ConfigureSwagger(app);
        }

        private IServiceProvider CreateAutofacServiceProvider(IServiceCollection services)
        {
            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new InfrastructureModule(_configuration[OrdersConnectionString]));
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ForeignExchangeModule());
            container.RegisterModule(new DomainModule());
            container.RegisterModule(new EmailModule());

            var children = this._configuration.GetSection("Caching").GetChildren();
            Dictionary<string, TimeSpan> configuration =
                children.ToDictionary(child => child.Key, child => TimeSpan.Parse(child.Value));
            container.RegisterModule(new CachingModule(configuration));

            var buildContainer = container.Build();
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(buildContainer));
            
            return new AutofacServiceProvider(buildContainer);
        }


        public void StartQuartz(IServiceProvider serviceProvider)
        {
            this._schedulerFactory = new StdSchedulerFactory();
            this._scheduler = _schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            var container = new ContainerBuilder();
            container.RegisterModule(new OutboxModule());
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new InfrastructureModule(this._configuration[OrdersConnectionString]));
            container.RegisterModule(new EmailModule());

            container.Register(c =>
            {
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<OrdersContext>();
                dbContextOptionsBuilder.UseSqlServer(this._configuration[OrdersConnectionString]);

                dbContextOptionsBuilder
                    .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

                return new OrdersContext(dbContextOptionsBuilder.Options);
            }).AsSelf().InstancePerLifetimeScope();

            _scheduler.JobFactory = new JobFactory(container.Build());

            _scheduler.Start().GetAwaiter().GetResult();

            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
            var trigger = 
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/15 * * ? * *")
                    .Build();

            _scheduler.ScheduleJob(processOutboxJob, trigger).GetAwaiter().GetResult(); 

            var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();
            var triggerCommandsProcessing = 
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/15 * * ? * *")
                    .Build();
            _scheduler.ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing).GetAwaiter().GetResult();      
        }


        private static void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample CQRS API V1");
            });
        }


        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Sample CQRS API",
                    Version = "v1",
                    Description = "Sample .NET Core REST API CQRS implementation with raw SQL and DDD using Clean Architecture."
                });

                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".XML";
                var commentsFile = Path.Combine(baseDirectory, commentsFileName);
                options.IncludeXmlComments(commentsFile);
            });
        }
    }
}
