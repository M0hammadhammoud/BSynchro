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
        //public DbSet<Transaction> Transactions { get; set; }
        //public DbSet<TransactionType> TransactionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //var userId = Guid.NewGuid();
            //var now = DateTime.UtcNow;

            //data seed when creating the db to load default data
            //modelBuilder.Entity<TransactionType>().HasData(new List<TransactionType>()
            //{
            //    new() { Id = 1, Name = "Credit", Description = "Amount added to the account" },
            //    new() { Id = 2, Name = "Debit", Description = "Amount withdrawn from the account" },
            //    new() { Id = 3, Name = "TransferIn", Description = "Funds transferred into this account" },
            //    new() { Id = 4, Name = "TransferOut", Description = "Funds transferred from this account" },
            //    new() { Id = 5, Name = "Refund", Description = "Refund of a previous transaction" },
            //    new() { Id = 6, Name = "Fee", Description = "Service or transaction fee" },
            //    new() { Id = 7, Name = "Chargeback", Description = "Chargeback on a previous transaction" },
            //    new() { Id = 8, Name = "Interest", Description = "Interest credited to the account" },
            //    new() { Id = 9, Name = "LoanDisbursement", Description = "Loan amount disbursed to the account" },
            //    new() { Id = 10, Name = "LoanRepayment", Description = "Repayment of loan from the account" },
            //    new() { Id = 11, Name = "Adjustment", Description = "Adjustment made to balance due to errors or corrections" }
            //});
        }
    }
}
