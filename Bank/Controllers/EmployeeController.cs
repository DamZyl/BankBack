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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
            => new JsonResult(await _employeeService.GetEmployeesAsync());
        
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(Guid id)
            => new JsonResult(await _employeeService.GetEmployeeByIdAsync(id));
        
        [HttpGet("email/{email}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeByMail(string email)
            => new JsonResult(await _employeeService.GetEmployeeByMailAsync(email));

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployee command)
        {
            await _employeeService.UpdateEmployeeAsync(id, command);

            return NoContent();
        }
    }
}