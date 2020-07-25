using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Application.Accounts.Services;
using Bank.Application.Accounts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers
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
        public async Task<ActionResult<IEnumerable<AccountDetailsViewModel>>> GetAccountsForCustomer(Guid customerId)
            => new JsonResult(await _accountService.GetCustomerAccountsAsync(customerId));
        
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDetailsViewModel>> GetAccountById(Guid id)
            => new JsonResult(await _accountService.GetAccountAsync(id));
    }
}