using BSynchro.RJP.Transactions.Domain.Entities;
using BSynchro.RJP.Transactions.Domain.Interfaces;
using BSynchro.RJP.Transactions.Infrastructure.Data;

namespace BSynchro.RJP.Transactions.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDbContext _transactionDbContext;

        public TransactionRepository(TransactionDbContext transactionDbContext)
        {
            _transactionDbContext = transactionDbContext;
        }

        public async Task AddAsync(Transaction transaction)
        {
            //_transactionDbContext.Transactions.Add(transaction);
            //await _transactionDbContext.SaveChangesAsync();
        }
    }
}
