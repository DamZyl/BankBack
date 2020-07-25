using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Application.Employees.Commands;
using Bank.Application.Employees.Services;
using Bank.Application.Employees.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers
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
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> GetEmployees()
            => new JsonResult(await _employeeService.GetEmployeesAsync());
        
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployeeById(Guid id)
            => new JsonResult(await _employeeService.GetEmployeeByIdAsync(id));
        
        [HttpGet("email/{email}")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployeeByMail(string email)
            => new JsonResult(await _employeeService.GetEmployeeByMailAsync(email));

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployee command)
        {
            await _employeeService.UpdateEmployeeAsync(id, command);

            return NoContent();
        }
    }
}