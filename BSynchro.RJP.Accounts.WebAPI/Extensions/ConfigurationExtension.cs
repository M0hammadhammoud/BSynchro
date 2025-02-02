using AutoMapper;
using BSynchro.RJP.Accounts.Application;
using BSynchro.RJP.Accounts.Infrastructure;
using BSynchro.RJP.Accounts.WebAPI.Mapping;
using BSynchro.RJP.Accounts.WebAPI.Middlewares;
using BSynchro.RJP.Accounts.WebAPI.Validators;
using Common.Utitlities.Contracts;
using Common.Utitlities.Helpers;
using Microsoft.AspNetCore.DataProtection;
using Serilog;
using FluentValidation;

namespace BSynchro.RJP.Accounts.WebAPI.Extensions
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

            // Register FluentValidation
            builder.Services.AddValidatorsFromAssemblyContaining<AccountValidator>();

            //services
            builder.Services.InjectServices(builder.Configuration);

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
            var dataProtectionProvider = DataProtectionProvider.Create("AccountsAPI");
            var protector = dataProtectionProvider.CreateProtector("BSynchro.RJP.Accounts.API");
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
