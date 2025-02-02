using BSynchro.RJP.Transactions.Domain.Constants;
using BSynchro.RJP.Transactions.Domain.Contracts;
using BSynchro.RJP.Transactions.Domain.Documents;
using Common.MongoDb.Contracts;

namespace BSynchro.RJP.Transactions.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDataHelper _dataHelper;

        public TransactionRepository(IDataHelper dataHelper)
        {
            _dataHelper = dataHelper;
        }

        public async Task<bool> AddTransactionAsync(Transaction transaction)
        {
            return await _dataHelper.AddNewRecord(transaction, Collections.Transactions);
        }

        public async Task<List<Transaction>> GetByAccountIdsAsync(List<Guid> accountIds)
        {
            var result = await _dataHelper.GetRecordsByCondition<Transaction>(x => accountIds.Contains(x.AccountId), x => x.TransactedOn, null, Collections.Transactions);

            return result.ToList();
        }
    }
}
