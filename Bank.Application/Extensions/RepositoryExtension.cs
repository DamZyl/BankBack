using System;
using System.Threading.Tasks;
using Bank.Domain.Repositories;
using Bank.Infrastructure.Exceptions;
using Bank.Domain.Models;
using Microsoft.EntityFrameworkCore;
using BankEntity = Bank.Domain.Models.Bank;

namespace Bank.Application.Extensions
{
    // refactor to generic
    public static class RepositoryExtension
    {
        public static async Task<Account> GetOrFailAccountAsync(this IGenericRepository<Account> repository, Guid id)
        {
            var account = await repository.FindByIdAsync(id);

            if (account == null)
            {
                throw new BusinessException(ErrorCodes.NoExist, "Account doesn't exist.");
            }

            return account;
        }
        
        public static async Task<Customer> GetOrFailCustomerAsync(this IGenericRepository<Customer> repository, Guid id)
        {
            var customer = await repository.FindByIdAsync(id);

            if (customer == null)
            {
                throw new BusinessException(ErrorCodes.NoExist,"Customer doesn't exist.");
            }

            return customer;
        }
        
        public static async Task<Customer> GetOrFailCustomerAsync(this IGenericRepository<Customer> repository, string email)
        {
            var customer = await repository.FindByAsync(x => x.Email == email);

            if (customer != null)
            {
                throw new BusinessException(ErrorCodes.Exist,"Customer exists.");
            }

            return null;
        }
        
        public static async Task<Employee> GetOrFailEmployeeAsync(this IGenericRepository<Employee> repository, Guid id)
        {
            var employee = await repository.FindByIdAsync(id);

            if (employee == null)
            {
                throw new BusinessException(ErrorCodes.NoExist,"Employee doesn't exist.");
            }

            return employee;
        }
        
        public static async Task<Employee> GetOrFailEmployeeAsync(this IGenericRepository<Employee> repository, string email)
        {
            var employee = await repository.FindByAsync(x => x.Email == email);

            if (employee != null)
            {
                throw new BusinessException(ErrorCodes.Exist,"Employee exists.");
            }

            return null;
        }
        
        public static async Task<BankEntity> GetOrFailBankAsync(this IGenericRepository<BankEntity> repository)
        {
            var bank = await repository.FindByWithIncludesAsync(null,
                includes: i => i.Include(x => x.Accounts));

            if (bank == null)
            {
                throw new BusinessException(ErrorCodes.NoExist,"Bank doesn't exist.");
            }

            return bank;
        }
    }
}