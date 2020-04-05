using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Models.Commands;
using Bank.Models.Dtos;

namespace Bank.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(Guid id);
        Task<EmployeeDto> GetEmployeeByMailAsync(string email);
        Task UpdateEmployeeAsync(Guid id, UpdateEmployee command);
    }
}