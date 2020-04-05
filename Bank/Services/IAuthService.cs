using System.Threading.Tasks;
using Bank.Infrastructure.Auth.Models;
using Bank.Models.Commands;

namespace Bank.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(Register command);
        Task<TokenDto> LoginAsync(Login command);
    }
}