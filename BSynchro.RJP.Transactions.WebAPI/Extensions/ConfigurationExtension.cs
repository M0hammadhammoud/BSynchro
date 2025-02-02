﻿using AutoMapper;
using BSynchro.RJP.Transactions.Application;
using BSynchro.RJP.Transactions.Infrastructure;
using BSynchro.RJP.Transactions.WebAPI.Mapping;
using BSynchro.RJP.Transactions.WebAPI.Middlewares;
using Common.MessageQueueSender.Models.DTOs;
using Common.Utitlities.Contracts;
using Common.Utitlities.Helpers;
using Microsoft.AspNetCore.DataProtection;
using Serilog;

namespace BSynchro.RJP.Transactions.WebAPI.Extensions
{
    public static class ConfigurationExtension
    {
        // this extension method defines some configuration for our solution and add DI and specify different lifetime for services
        public static void InjectServicesAndRepositories(this IServiceCollection services, WebApplicationBuilder builder)
        {
            //serilog configuration
            builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

            //Add IHttpContextAccessor
            services.AddHttpContextAccessor();

            //configure data protector
            var dataProtector = builder.Services.ConfigureDataProtector();

            builder.Services.AddTransient(sp => builder.Configuration.GetSection("RabbitMq").Get<RabbitMqConfigurationDTO>());

            //services
            builder.Services.InjectServices();

            //repositories
            builder.Services.InjectRepositories(builder.Configuration);

            //autmapper
            builder.Services.ConfigureAutoMapper(dataProtector);

            // IN-MEMORY CACHE CONFIGURATION
            services.AddSingleton<ICacheManager, CacheManager>();
            services.AddMemoryCache();
        }

        public static IDataProtector ConfigureDataProtector(this IServiceCollection services)
        {
            var dataProtectionProvider = DataProtectionProvider.Create("TransactionsAPI");
            var protector = dataProtectionProvider.CreateProtector("BSynchro.RJP.Transactions.API");
            services.AddSingleton(protector);

            return protector;
        }

        public static void UseCustomInterceptorMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<InterceptorMiddleware>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services, IDataProtector dataProtector)
        {
            var mappingConfiguration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile(dataProtector));
                mc.AddProfile(new Application.Mapping.MappingProfile(dataProtector));
            });

            var mapper = mappingConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
