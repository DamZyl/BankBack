using System.Threading.Tasks;
using Bank.Infrastructure.Auth.Models;
using Bank.Models.Commands;
using Bank.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> Login([FromBody] Login command)
            => new JsonResult(await _authService.LoginAsync(command));

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] CreateCustomer command)
        {
            await _authService.RegisterAsync(command);
            
            return CreatedAtAction(null, null, null);
        }
    }
}