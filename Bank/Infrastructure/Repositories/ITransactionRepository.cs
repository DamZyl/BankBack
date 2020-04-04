using System.Threading.Tasks;
using Bank.Models;

namespace Bank.Infrastructure.Repositories
{
    public interface ITransactionRepository
    {
        Task AddTransactionAsync(Transaction transaction);
    }
}