using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BSynchro.RJP.Accounts.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public int TransactionTypeId { get; set; }

        // Navigation Properties
        public Account Account { get; set; } = null!;
        public TransactionType TransactionType { get; set; } = null!;
    }
}
