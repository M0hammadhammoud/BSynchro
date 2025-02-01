using AutoMapper;
using BSynchro.RJP.Accounts.Application.Contracts;
using BSynchro.RJP.Accounts.Application.Mapping;
using BSynchro.RJP.Accounts.Application.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace BSynchro.RJP.Accounts.Application
{
    public static class DependencyInjection
    {
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestInfoService, RequestInfoService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITransactionService, TransactionService>();
        }   
    }
}
