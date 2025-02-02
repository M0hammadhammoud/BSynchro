namespace BSynchro.RJP.Transactions.WebAPI.Models.Requests.Transactions
{
    public class GetTransactionsRequest
    {
        public required List<Guid> AccountIds { get; set; }
    }
}
