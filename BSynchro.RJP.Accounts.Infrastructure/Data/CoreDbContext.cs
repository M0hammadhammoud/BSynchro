using BSynchro.RJP.Accounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BSynchro.RJP.Accounts.Infrastructure.Data
{
    //out db context that have our db definition and this is the context used to perform db migrations using code first approach
    public class CoreDBContext : DbContext
    {
        public CoreDBContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
