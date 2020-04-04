using System.Threading.Tasks;
using Bank.Models.Dtos;
using Bank.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankService _bankService;

        public BankController(IBankService bankService)
        {
            _bankService = bankService;
        }

        [HttpGet]
        public async Task<ActionResult<BankDetailsDto>> GetAsync()
            => new JsonResult(await _bankService.GetInfoAsync());
    }
}