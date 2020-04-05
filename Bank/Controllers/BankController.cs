using System;
using System.Threading.Tasks;
using Bank.Models.Commands;
using Bank.Models.Dtos;
using Bank.Services;
using Microsoft.AspNetCore.Authorization;
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

        //[Authorize(Roles = "Employee, Admin, Customer")]
        [HttpGet]
        public async Task<ActionResult<BankDetailsDto>> Get()
            => new JsonResult(await _bankService.GetInfoAsync());
        
        [HttpPost("customer")]
        public async Task<ActionResult> CreateCustomer(CreateCustomer command)
        {
            await _bankService.CreateCustomerAsync(command);

            return CreatedAtAction(null, null, null);
        }

        [HttpDelete("customer/{id}")]
        public async Task<ActionResult> DeleteCustomer(Guid id)
        {
            await _bankService.DeleteCustomerAsync(id);

            return NoContent();
        }

        [HttpPost("{customerId}/account")]
        public async Task<ActionResult> CreateAccount(CreateAccount command)
        {
            await _bankService.CreateAccountAsync(command);

            return CreatedAtAction(null, null, null);
        }

        [HttpDelete("{customerId}/account/{id}")]
        public async Task<ActionResult> DeleteAccount(Guid id)
        {
            await _bankService.DeleteAccountAsync(id);

            return NoContent();
        }
    }
}