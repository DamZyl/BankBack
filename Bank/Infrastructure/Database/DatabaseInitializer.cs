using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Models;
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
                await _bankContext.AddAsync(new BankEntity
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
                });
            }

            if (!_bankContext.Customers.Any())
            {
                await _bankContext.AddAsync(new Customer
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
                    
                });
            }

            if (!_bankContext.Customers.Any())
            {
                await _bankContext.AddRangeAsync(AddAccounts());
            }

            await _bankContext.SaveChangesAsync();
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
    }
}