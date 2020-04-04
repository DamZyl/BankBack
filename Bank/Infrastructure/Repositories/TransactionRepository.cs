using System.Threading.Tasks;
using Bank.Infrastructure.Database;
using Bank.Models;

namespace Bank.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankContext _bankContext;

        public TransactionRepository(BankContext bankContext)
        {
            _bankContext = bankContext;
        }
        
        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _bankContext.Transactions.AddAsync(transaction);
            await _bankContext.SaveChangesAsync();
        }
    }
}