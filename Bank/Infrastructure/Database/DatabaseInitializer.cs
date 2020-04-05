using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Models;
using Bank.Models.Enums;
using BankEntity = Bank.Models.Bank;

namespace Bank.Infrastructure.Database
{
    public class DatabaseInitializer
    {
        private readonly BankContext _bankContext;

        public DatabaseInitializer(BankContext bankContext)
        {
            _bankContext = bankContext;
        }

        public async Task SeedData()
        {
            if (!_bankContext.Banks.Any())
            {
                await _bankContext.AddAsync(AddBank());
            }

            if (!_bankContext.Customers.Any())
            {
                await _bankContext.AddAsync(AddCustomer());
            }

            if (!_bankContext.Customers.Any())
            {
                await _bankContext.AddRangeAsync(AddAccounts());
            }

            if (!_bankContext.Transactions.Any())
            {
                await _bankContext.AddRangeAsync(AddTransactions());
            }

            await _bankContext.SaveChangesAsync();
        }

        #region MockDb

        private static BankEntity AddBank()
        {
            return new BankEntity
            {
                Id = Guid.Parse("6C0BF466-EDFF-4DFD-912C-BF78C89C9D0B"),
                Name = "ING",
                Address = new Address
                {
                    Street = "Marszałkowska",
                    Number = "15",
                    PostCode = "00-480",
                    City = "Warshaw",
                    Country = "Poland"
                }
            };
        }

        private static Customer AddCustomer()
        {
            return new Customer
            {
                Id = Guid.Parse("90F14D37-D730-4307-9BC7-19D92A16A662"),
                FirstName = "Arkadiusz",
                LastName = "Nowak",
                Address = new Address
                {
                    Street = "Bema",
                    Number = "11/15",
                    PostCode = "01-450",
                    City = "Warshaw",
                    Country = "Poland"
                },
                Email = "anowak@gmail.com",
                PhoneNumber = "515-098-789",
            };
        }

        private static IEnumerable<Account> AddAccounts()
        {
            return new List<Account>
            {
                new Account
                {
                    Id = Guid.Parse("21C350E8-E3C7-425C-8335-BDEEDD9ACA89"),
                    BankId = Guid.Parse("6C0BF466-EDFF-4DFD-912C-BF78C89C9D0B"),
                    CustomerId = Guid.Parse("90F14D37-D730-4307-9BC7-19D92A16A662"),
                    AccountNumber = "PL61 1090 1014 0000 0712 1981 2874",
                    Balance = 900M,
                },
                new Account
                {
                    Id = Guid.Parse("C2827041-8054-4621-be89-FD7F7FA9F142"),
                    BankId = Guid.Parse("6C0BF466-EDFF-4DFD-912C-BF78C89C9D0B"),
                    CustomerId = Guid.Parse("90F14D37-D730-4307-9BC7-19D92A16A662"),
                    AccountNumber = "PL61 1490 1014 0000 0712 1981 2874",
                    Balance = 800M,
                },
            };
        }

        private static IEnumerable<Transaction> AddTransactions()
        {
            return new List<Transaction>
            {
                new Transaction
                {
                    Id = Guid.Parse("03351351-6650-4B9A-9904-B5F1C4CCEE6C"),
                    AccountId = Guid.Parse("21C350E8-E3C7-425C-8335-BDEEDD9ACA89"),
                    Date = DateTime.Now.AddDays(-5),
                    TransactionType = TransactionType.Income,
                    Description = "Wynagrodzenie",
                    Value = 1800M
                },
                new Transaction
                {
                    Id = Guid.Parse("E8153A45-6F4B-4234-8934-1F7D603F534F"),
                    AccountId = Guid.Parse("21C350E8-E3C7-425C-8335-BDEEDD9ACA89"),
                    Date = DateTime.Now.AddDays(-3),
                    TransactionType = TransactionType.Income,
                    Description = "Premia",
                    Value = 200M
                },
                new Transaction
                {
                    Id = Guid.Parse("D19DB5D8-3A9F-49A9-BA16-7E04AB3ED0BD"),
                    AccountId = Guid.Parse("21C350E8-E3C7-425C-8335-BDEEDD9ACA89"),
                    Date = DateTime.Now.AddDays(-1),
                    TransactionType = TransactionType.Outcome,
                    Description = "Zakupy",
                    Value = 800M
                },
                new Transaction
                {
                    Id = Guid.Parse("6A6351DB-93E9-48C5-BBD5-0B03844C1026"),
                    AccountId = Guid.Parse("21C350E8-E3C7-425C-8335-BDEEDD9ACA89"),
                    Date = DateTime.Now,
                    TransactionType = TransactionType.Outcome,
                    Description = "Zwrot kosztów",
                    Value = 300M
                }
            };
        }

        #endregion
        
    }
}