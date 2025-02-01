using AutoMapper;
using BSynchro.RJP.Accounts.Application;
using BSynchro.RJP.Accounts.Application.Mapping;
using BSynchro.RJP.Accounts.Infrastructure;
using BSynchro.RJP.Accounts.WebAPI.Middlewares;
using Common.Utitlities.Contracts;
using Common.Utitlities.Helpers;
using Microsoft.AspNetCore.DataProtection;
using Serilog;

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

            //services
            builder.Services.InjectServices();

            //repositories
            builder.Services.InjectRepositories(builder.Configuration);

            //autmapper
            builder.Services.ConfigureAutoMapper();

            // IN-MEMORY CACHE CONFIGURATION
            services.AddSingleton<ICacheManager, CacheManager>();
            services.AddMemoryCache();
        }

        public static void ConfigureDataProtector(this IServiceCollection services)
        {
            var dataProtectionProvider = DataProtectionProvider.Create("AccountsAPI");
            var protector = dataProtectionProvider.CreateProtector("Accounts.API");
            services.AddSingleton(protector);
        }
       
        public static void UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<InterceptorMiddleware>();
        }
    }
}
