using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Infrastructure.Auth;
using Bank.Models;
using Bank.Models.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BankEntity = Bank.Models.Bank;

namespace Bank.Infrastructure.Database
{
    public static class DatabaseInitializer
    {
        public static async Task PrepPopulation(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            await SeedData(serviceScope.ServiceProvider.GetService<BankContext>(), serviceScope.ServiceProvider.GetService<IPasswordHasher>());
        }

        private static async Task SeedData(BankContext bankContext, IPasswordHasher passwordHasher)
        {
            Console.WriteLine("Appling Migrations...");
            
            await bankContext.Database.MigrateAsync();

            if (!bankContext.Banks.Any())
            {
                Console.WriteLine("Adding data to bank - seeding...");
                await bankContext.AddAsync(AddBank());
            }

            if (!bankContext.Customers.Any())
            {
                Console.WriteLine("Adding data to customers - seeding...");
                await bankContext.AddAsync(AddCustomer(passwordHasher));
            }
            
            if (!bankContext.Employees.Any())
            {
                Console.WriteLine("Adding data to employees - seeding...");
                await bankContext.AddRangeAsync(AddEmployees(passwordHasher));
            }

            if (!bankContext.Accounts.Any())
            {
                Console.WriteLine("Adding data to accounts - seeding...");
                await bankContext.AddRangeAsync(AddAccounts());
            }

            if (!bankContext.Transactions.Any())
            {
                Console.WriteLine("Adding data to transactions - seeding...");
                await bankContext.AddRangeAsync(AddTransactions());
            }

            await bankContext.SaveChangesAsync();
            Console.WriteLine("Already have data - not seeding");
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

        private static Customer AddCustomer(IPasswordHasher passwordHasher)
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
                Password = passwordHasher.Hash("customer111"),
                RoleInSystem = RoleType.Customer
            };
        }
        
        private static IEnumerable<Employee> AddEmployees(IPasswordHasher passwordHasher)
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = Guid.Parse("DB5118DD-E555-44E8-B3CE-E0AB20C8E809"),
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Email = "jkowalski@gmail.com",
                    PhoneNumber = "545-098-789",
                    Password = passwordHasher.Hash("admin111"),
                    RoleInSystem = RoleType.Admin,
                    Position = "Administrator"
                },
                new Employee
                {
                    Id = Guid.Parse("22B20AF3-817D-4F86-A77E-08D7EAEEA477"),
                    FirstName = "Marcin",
                    LastName = "Nowak",
                    Email = "mnowak@gmail.com",
                    PhoneNumber = "789-951-741",
                    Password = passwordHasher.Hash("employee111"),
                    RoleInSystem = RoleType.Employee,
                    Position = "Manager"
                }
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
                },
                new Transaction
                {
                    Id = Guid.Parse("864EC7AF-5696-4F08-AB7A-57C3D65D3B8F"),
                    AccountId = Guid.Parse("C2827041-8054-4621-be89-FD7F7FA9F142"),
                    Date = DateTime.Now,
                    TransactionType = TransactionType.Income,
                    Description = "Wynagrodzenie",
                    Value = 800M
                }
            };
        }

        #endregion
        
    }
}