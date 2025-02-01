namespace BSynchro.RJP.Accounts.WebAPI.Models.Requests.Account
{
    public class OpenAccountRequest
    {
        public required string CustomerId { get; set; }
        public decimal InitialCredit { get; set; } = 0;
    }
}
