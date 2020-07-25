using System.Threading.Tasks;
using Bank.Application.Auths.Commands;

namespace Bank.Application.Auths.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(CreateCustomer command);
        Task<string> LoginAsync(Login command);
    }
}