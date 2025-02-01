using BSynchro.RJP.Accounts.Domain.Contracts;
using BSynchro.RJP.Accounts.Infrastructure.Data;

namespace BSynchro.RJP.Accounts.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly CoreDBContext _coreDBContext;
        public TransactionRepository(CoreDBContext coreDBContext)
        {
            _coreDBContext = coreDBContext;
        }
    }
}
