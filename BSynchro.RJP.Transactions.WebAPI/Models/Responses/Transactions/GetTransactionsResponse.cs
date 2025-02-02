using BSynchro.RJP.Transactions.Application.Models.DTOs;

namespace BSynchro.RJP.Transactions.WebAPI.Models.Responses.Transactions
{
    public class GetTransactionsResponse : BaseResponse
    {
        public List<TransactionDTO> Transactions { get; set; } = new();
    }
}
