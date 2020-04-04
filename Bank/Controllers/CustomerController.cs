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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDetailsDto>>> GetCustomers()
            => new JsonResult(await _customerService.GetCustomersAsync());
        
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDetailsDto>> GetCustomerById(Guid id)
            => new JsonResult(await _customerService.GetCustomerByIdAsync(id));
        
        [HttpGet("email/{email}")]
        public async Task<ActionResult<CustomerDetailsDto>> GetCustomerByMail(string email)
            => new JsonResult(await _customerService.GetCustomerByMailAsync(email));

        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromBody] CreateCustomer command)
        {
            await _customerService.CreateCustomerAsync(command);

            return CreatedAtAction(null, null, null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(Guid id, [FromBody] UpdateCustomer command)
        {
            await _customerService.UpdateCustomerAsync(id, command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(Guid id)
        {
            await _customerService.DeleteCustomerAsync(id);

            return NoContent();
        }
    }
}