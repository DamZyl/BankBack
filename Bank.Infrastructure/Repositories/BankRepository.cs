using System.Threading.Tasks;
using Bank.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using BankEntity = Bank.Domain.Models.Bank;
using Bank.Domain.Repositories;

namespace Bank.Infrastructure.Repositories
{
    public class BankRepository : IBankRepository
    {
        private readonly BankContext _bankContext;
        
        public BankRepository(BankContext bankContext)
        {
            _bankContext = bankContext;
        }

        public async Task<BankEntity> GetInfoAsync()
            => await _bankContext.Banks
                .Include(x => x.Accounts)
                .SingleOrDefaultAsync();
    }
}