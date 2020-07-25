using System.Threading.Tasks;
using Bank.Application.Accounts.Commands;
using Bank.Application.Accounts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public TransactionController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("{id}/transaction")]
        public async Task<ActionResult> CreateTransaction([FromBody] CreateTransaction command)
        {
            await _accountService.CreateTransactionAsync(command);

            return CreatedAtAction(null, null, null);
        }
    }
}