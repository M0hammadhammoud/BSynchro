using System.ComponentModel.DataAnnotations.Schema;

namespace BSynchro.RJP.Accounts.Domain.Entities
{
    public class TransactionType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = [];
    }
}
