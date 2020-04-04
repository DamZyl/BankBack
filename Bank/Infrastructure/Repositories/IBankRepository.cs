using System.Threading.Tasks;
using BankEntity = Bank.Models.Bank;

namespace Bank.Infrastructure.Repositories
{
    public interface IBankRepository
    {
        Task<BankEntity> GetInfoAsync();
    }
}