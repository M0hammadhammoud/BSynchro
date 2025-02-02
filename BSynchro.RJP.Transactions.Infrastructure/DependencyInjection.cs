using BSynchro.RJP.Transactions.Domain.Contracts;
using BSynchro.RJP.Transactions.Infrastructure.Repositories;
using Common.MongoDb.Contexts;
using Common.MongoDb.Contracts;
using Common.MongoDb.Helpers;
using Common.MongoDb.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BSynchro.RJP.Transactions.Infrastructure
{
    public static class DependencyInjection
    {
        public static void InjectRepositories(this IServiceCollection services, IConfigurationManager configurationManager)
        {
            services.AddTransient<IMongoDbSettings>(sp => configurationManager.GetSection("MongoDbSettings").Get<MongoDbSettings>());

            services.AddTransient<MongoDbContext>();
            services.AddTransient<IDataHelper, DataHelper>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
        }
    }
}
