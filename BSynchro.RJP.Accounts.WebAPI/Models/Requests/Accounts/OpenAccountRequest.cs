namespace BSynchro.RJP.Accounts.WebAPI.Models.Requests.Accounts
{
    public class OpenAccountRequest
    {
        public required string CustomerId { get; set; }
        public decimal InitialCredit { get; set; } = 0;
    }
}
