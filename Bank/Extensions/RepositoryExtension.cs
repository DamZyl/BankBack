using System;
using System.Threading.Tasks;
using Bank.Infrastructure.Repositories;
using Bank.Models;
using BankEntity = Bank.Models.Bank;

namespace Bank.Extensions
{
    public static class RepositoryExtension
    {
        public static async Task<Account> GetOrFailAsync(this IAccountRepository repository, Guid id)
        {
            var account = await repository.GetAccountAsync(id);

            if (account == null)
            {
                throw new Exception("Account doesn't exist.");
            }

            return account;
        }
        
        public static async Task<Customer> GetOrFailAsync(this ICustomerRepository repository, Guid id)
        {
            var customer = await repository.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                throw new Exception("Customer doesn't exist.");
            }

            return customer;
        }
        
        public static async Task<Customer> GetOrFailAsync(this ICustomerRepository repository, string email)
        {
            var customer = await repository.GetCustomerByMailAsync(email);

            if (customer != null)
            {
                throw new Exception("Customer exists.");
            }

            return null;
        }
        
        public static async Task<BankEntity> GetOrFailAsync(this IBankRepository repository)
        {
            var bank = await repository.GetInfoAsync();

            if (bank == null)
            {
                throw new Exception("Bank doesn't exist.");
            }

            return bank;
        }
    }
}