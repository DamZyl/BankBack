using System.Threading.Tasks;
using Bank.Models.Commands;
using Bank.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateTransaction([FromBody] CreateTransaction command)
        {
            await _transactionService.CreateTransactionAsync(command);

            return CreatedAtAction(null, null, null);
        }
    }
}