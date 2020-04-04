using System.Threading.Tasks;
using Bank.Models.Dtos;

namespace Bank.Services
{
    public interface IBankService
    {
        Task<BankDetailsDto> GetInfoAsync();
    }
}