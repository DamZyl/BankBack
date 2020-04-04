using Bank.Models;
using Microsoft.EntityFrameworkCore;
using BankEntity = Bank.Models.Bank;

namespace Bank.Infrastructure.Database
{
    public class BankContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<BankEntity> Banks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        
        public BankContext(DbContextOptions<BankContext> options) : base(options) {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Ignore

            modelBuilder.Ignore<Address>();

            #endregion
            
            #region BankConfig

            modelBuilder.Entity<BankEntity>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<BankEntity>(tab =>
                tab.OwnsOne(
                    x => x.Address,
                    address =>
                    {
                        address.Property(x => x.Street).HasColumnName("Street");
                        address.Property(x => x.Number).HasColumnName("Number");
                        address.Property(x => x.PostCode).HasColumnName("PostCode");
                        address.Property(x => x.City).HasColumnName("City");
                        address.Property(x => x.Country).HasColumnName("Country");
                    }
                ));
            
            #endregion

            #region AccountConfig

            modelBuilder.Entity<Account>()
                .HasKey(x => x.Id);
            
            modelBuilder.Entity<Account>()
                .HasOne(x => x.Bank)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.BankId);
            
            modelBuilder.Entity<Account>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.CustomerId);

            #endregion

            #region CustomerConfig

            modelBuilder.Entity<Customer>()
                .HasKey(x => x.Id);
            
            modelBuilder.Entity<Customer>(tab =>
                tab.OwnsOne(
                    x => x.Address,
                    address =>
                    {
                        address.Property(x => x.Street).HasColumnName("Street");
                        address.Property(x => x.Number).HasColumnName("Number");
                        address.Property(x => x.PostCode).HasColumnName("PostCode");
                        address.Property(x => x.City).HasColumnName("City");
                        address.Property(x => x.Country).HasColumnName("Country");
                    }
                ));

            #endregion

            #region TransactionConfig

            modelBuilder.Entity<Transaction>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Transaction>()
                .HasOne(x => x.Account)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.AccountId);
            
            modelBuilder.Entity<Transaction>()
                .Property(x => x.TransactionType)
                .HasConversion<string>();

            #endregion
        }
    }
}