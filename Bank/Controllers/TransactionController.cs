using System.Threading.Tasks;
using Bank.Models.Commands;
using Bank.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
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