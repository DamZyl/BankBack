using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Application.Customers.Commands;
using Bank.Application.Customers.Services;
using Bank.Application.Customers.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDetailsViewModel>>> GetCustomers()
            => new JsonResult(await _customerService.GetCustomersAsync());
        
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDetailsViewModel>> GetCustomerById(Guid id)
            => new JsonResult(await _customerService.GetCustomerByIdAsync(id));
        
        [HttpGet("email/{email}")]
        public async Task<ActionResult<CustomerDetailsViewModel>> GetCustomerByMail(string email)
            => new JsonResult(await _customerService.GetCustomerByMailAsync(email));

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(Guid id, [FromBody] UpdateCustomer command)
        {
            await _customerService.UpdateCustomerAsync(id, command);

            return NoContent();
        }
    }
}