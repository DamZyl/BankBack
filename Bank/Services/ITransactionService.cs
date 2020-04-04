using System.Threading.Tasks;
using Bank.Models.Commands;

namespace Bank.Services
{
    public interface ITransactionService
    {
        Task CreateTransactionAsync(CreateTransaction command);
    }
}