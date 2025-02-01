using BSynchro.RJP.Accounts.Domain.Contracts;
using BSynchro.RJP.Accounts.Infrastructure.Data;
using BSynchro.RJP.Accounts.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BSynchro.RJP.Accounts.Infrastructure
{
    public static class DependencyInjection
    {
        public static void InjectRepositories(this IServiceCollection services, IConfigurationManager configurationManager)
        {
            var connectionString = configurationManager.GetConnectionString("BSynchroDatabase");

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddDbContext<CoreDBContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
