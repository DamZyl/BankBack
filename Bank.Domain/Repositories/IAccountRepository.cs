using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Domain.Models;

namespace Bank.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetCustomerAccountsAsync(Guid customerId);
        Task<Account> GetAccountAsync(Guid id);
        Task AddAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(Account account);
    }
}