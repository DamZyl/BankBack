using Bank.Infrastructure.Database.Configurations;
using Bank.Domain.Models;
using Bank.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using BankEntity = Bank.Domain.Models.Bank;

namespace Bank.Infrastructure.Database
{
    public class BankContext : DbContext
    {
        private readonly IOptions<SqlOptions> _sqlOptions;
        
        public DbSet<Account> Accounts { get; set; }
        public DbSet<BankEntity> Banks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public BankContext(IOptions<SqlOptions> sqlOptions)
        {
            _sqlOptions = sqlOptions;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            optionsBuilder.UseSqlServer(_sqlOptions.Value.ConnectionString, options => options.MigrationsAssembly("Bank.Api"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BankConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        }
    }
}