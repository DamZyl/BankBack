using System.Threading.Tasks;
using Bank.Infrastructure.Repositories;
using Bank.Models.Commands;

namespace Bank.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        
        public async Task CreateTransactionAsync(CreateTransaction command)
        {
            await Task.CompletedTask;
        }
    }
}