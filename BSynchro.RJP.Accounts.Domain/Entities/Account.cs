using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BSynchro.RJP.Accounts.Domain.Entities
{
    public class Account : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid AccountId { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public Customer Customer { get; set; } = null!;
    }
}
