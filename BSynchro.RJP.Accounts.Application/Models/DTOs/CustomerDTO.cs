namespace BSynchro.RJP.Accounts.Application.Models.DTOs
{
    public class CustomerDTO
    {
        public required string CustomerId { get; set; }
        public Guid UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
