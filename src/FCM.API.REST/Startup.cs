using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Model;
using FCM.Application;
using FCM.API.REST.Providers;
using AutoMapper;
using Framework.DomainModel.Repositories;
using Framework.Infrastructure.Repositories.EF;
using FCM.Repositories.EF;
using FCM.DomainModel.Repositories;
using FCM.Repositories.EF.SQLServer;
using Framework.Infrastruture;
using FCM.Repositories.EF.EFInMemory;

namespace FCM.API.REST
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSwaggerGen();
            services.AddAutoMapper();
            this.AddSwaggerConfigurations(services);

            services.AddSingleton<IFCMApplication, FCMApplication>();
            services.AddTransient<IUnitOfWork, EFUnitOfWork<FCMContext>>();

            var dbProvider = Configuration.GetValue<string>("dbProvider");

            if (dbProvider == "EFSQLServer")
            {
                services.AddSingleton<IUnitOfWorkFactory<IUnitOfWork>, EFSQLServerFCMUnitOfWorkFactory>();
            }
            else if (dbProvider == "EFInMemory")
            {
                services.AddSingleton<IUnitOfWorkFactory<IUnitOfWork>, EFInMemoryFCMUnitOfWorkFactory>();
            }
            else
            {
                throw new Exception("Invalid appSetting key: dbProvider");
            }

            services.AddSingleton<IFCMRepositoryContainerFactory, EFFCMRepositoryContainerFactory>();
            services.AddSingleton<Framework.Infrastruture.IConfigurationProvider, AppSettingsConfigurationProvider>();
            services.AddSingleton<IRequestIdGenerator, GuidRequestIdGenerator>();
            services.AddSingleton<IAuthenticationProvider, FCMAuthenticationProvider>();
            services.AddSingleton<INotificationService, NotificationService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUi();

            app.UseMvc();
        }

        private void AddSwaggerConfigurations(IServiceCollection services)
        {
            // Inject an implementation of ISwaggerProvider with defaulted settings applied



            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                    {
                        Version = "v1",
                        Title = "FCM",
                        Description = "FCM",
                        TermsOfService = "None"
                    }
                );

                options.DescribeAllEnumsAsStrings();

                //options.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "FCM.API.REST.xml"));
            });
        }
    }
}
