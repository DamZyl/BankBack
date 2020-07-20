using System;
using System.Threading.Tasks;
using Bank.Application.Models.Commands;
using Bank.Application.Models.ViewModels;
using Bank.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers
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
        public async Task<ActionResult<BankDetailsViewModel>> Get()
            => new JsonResult(await _bankService.GetInfoAsync());
        
        [HttpPost("customer")]
        public async Task<ActionResult> CreateCustomer([FromBody] CreateCustomer command)
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
        
        [HttpPost("employee")]
        public async Task<ActionResult> CreateEmployee([FromBody] CreateEmployee command)
        {
            await _bankService.CreateEmployeeAsync(command);

            return CreatedAtAction(null, null, null);
        }

        [HttpDelete("employee/{id}")]
        public async Task<ActionResult> DeleteEmployee(Guid id)
        {
            await _bankService.DeleteEmployeeAsync(id);

            return NoContent();
        }

        [HttpPost("{customerId}/account")]
        public async Task<ActionResult> CreateAccount([FromBody] CreateAccount command)
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