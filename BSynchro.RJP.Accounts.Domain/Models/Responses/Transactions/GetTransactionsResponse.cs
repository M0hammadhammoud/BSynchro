using BSynchro.RJP.Accounts.Domain.Models.DTOs.Transactions;

namespace BSynchro.RJP.Accounts.Domain.Models.Responses.Transactions
{
    public class GetTransactionsResponse : BaseResponse
    {
        public List<TransactionDTO> Transactions { get; set; } = [];
    }
}