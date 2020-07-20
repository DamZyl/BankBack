using System.Threading.Tasks;
using Bank.Application.Models.Commands;

namespace Bank.Application.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(CreateCustomer command);
        Task<string> LoginAsync(Login command);
    }
}