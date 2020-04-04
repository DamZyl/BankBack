using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Infrastructure.Database;
using Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankContext _bankContext;

        public AccountRepository(BankContext bankContext)
        {
            _bankContext = bankContext;
        }

        public async Task<IEnumerable<Account>> GetCustomerAccountsAsync(Guid customerId)
            => await _bankContext.Accounts
                .Include(x => x.Transactions)
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();

        public async Task<Account> GetAccountAsync(Guid id)
            => await _bankContext.Accounts
                .Include(x => x.Transactions)
                .SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddAccountAsync(Account account)
        {
            await _bankContext.Accounts.AddAsync(account);
            await _bankContext.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            _bankContext.Accounts.Update(account);
            await _bankContext.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(Account account)
        {
            _bankContext.Accounts.Remove(account);
            await _bankContext.SaveChangesAsync();
        }
    }
}