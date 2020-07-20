using System.Threading.Tasks;
using BankEntity = Bank.Domain.Models.Bank;

namespace Bank.Domain.Repositories
{
    public interface IBankRepository
    {
        Task<BankEntity> GetInfoAsync();
    }
}