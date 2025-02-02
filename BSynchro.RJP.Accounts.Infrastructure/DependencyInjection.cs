using BSynchro.RJP.Accounts.Domain.Contracts;
using BSynchro.RJP.Accounts.Infrastructure.Data;
using BSynchro.RJP.Accounts.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BSynchro.RJP.Accounts.Domain.Models.DTOs.Configurations;
using BSynchro.RJP.Accounts.Domain.Enums;
using Common.Utitlities.Contracts;
using Common.Utitlities.Helpers;
using BSynchro.RJP.Accounts.Infrastructure.ClientServices;

namespace BSynchro.RJP.Accounts.Infrastructure
{
    public static class DependencyInjection
    {
        public static void InjectRepositories(this IServiceCollection services, IConfigurationManager configurationManager)
        {
            var connectionString = configurationManager.GetConnectionString("BSynchroDatabase");
            services.AddTransient(sp => configurationManager.GetSection("TransactionsSettings").Get<TransactionsSettingsDTO>()!);

            var clientSettings = configurationManager.GetSection("ClientsSettings").Get<List<ClientSettingsDTO>>();

            services.AddHttpClient<IHttpClientHelper, HttpClientHelper>(nameof(HttpClientsEnum.Transactions), client =>
            {
                client.BaseAddress = new Uri(clientSettings.First(c => c.ClientName == nameof(HttpClientsEnum.Transactions)).BaseUrl);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ITransactionsClientService, TransactionsClientService>();

            services.AddDbContext<CoreDBContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
