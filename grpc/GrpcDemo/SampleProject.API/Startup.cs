using System;
using System.IO;
using System.Reflection;
using Autofac;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SampleProject.API.SeedWork;
using SampleProject.Domain.SeedWork;
using SampleProject.Infrastructure;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.API
{
    public class Startup
    {
        public IConfiguration _Configuration;
        public ILifetimeScope AutofacContainer;
        private const string OrdersConnectionString = nameof(OrdersConnectionString);


        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            AddSwagger(services);

            services
                .AddEntityFrameworkSqlServer()

                .AddDbContext<OrdersContext>(options =>
                {
                    options
                        .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>()

                        .UseSqlServer(this._Configuration[OrdersConnectionString]);
                });

            services.AddProblemDetails(x =>
            {
                x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
            });

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env, 
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseProblemDetails();
            }

            ConfigureSwagger(app);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
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
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sample CQRS API",
                    Version = "v1",
                    Description = "Sample .NET Core REST API CQRS implementation with raw SQL and DDD using Clean Architecture.",
                });

                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".XML";
                var commentsFile = Path.Combine(baseDirectory, commentsFileName);
                options.IncludeXmlComments(commentsFile);
            });
        }
    }
}
