using BSynchro.RJP.Transactions.Application.Contracts;
using BSynchro.RJP.Transactions.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BSynchro.RJP.Transactions.Application
{
    public static class DependencyInjection
    {
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestInfoService, RequestInfoService>();
            services.AddTransient<ITransactionService, TransactionService>();
        }
    }
}
