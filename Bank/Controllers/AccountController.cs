using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Models.Commands;
using Bank.Models.Dtos;
using Bank.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<AccountDetailsDto>>> GetAccountsForCustomer(Guid customerId)
            => new JsonResult(await _accountService.GetCustomerAccountsAsync(customerId));
        
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDetailsDto>> GetAccountById(Guid id)
            => new JsonResult(await _accountService.GetAccountAsync(id));
        
        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromBody] CreateAccount command)
        {
            await _accountService.CreateAccountAsync(command);

            return CreatedAtAction(null, null, null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(Guid id, [FromBody] UpdateAccount command)
        {
            await _accountService.UpdateAccountAsync(id, command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(Guid id)
        {
            await _accountService.DeleteAccountAsync(id);

            return NoContent();
        }
    }
}