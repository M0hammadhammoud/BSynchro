using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BSynchro.RJP.Accounts.Domain.Entities
{
    public class Customer : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Account> Accounts { get; set; } = [];
    }
}
