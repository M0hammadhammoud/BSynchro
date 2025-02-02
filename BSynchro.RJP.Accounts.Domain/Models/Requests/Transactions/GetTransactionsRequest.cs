namespace BSynchro.RJP.Accounts.Domain.Models.Requests.Transactions
{
    public class GetTransactionsRequest
    {
        public required List<Guid> AccountIds { get; set; }
    }
}
