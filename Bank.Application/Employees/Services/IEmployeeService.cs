using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Application.Employees.Commands;
using Bank.Application.Employees.ViewModels;

namespace Bank.Application.Employees.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeViewModel>> GetEmployeesAsync();
        Task<EmployeeViewModel> GetEmployeeByIdAsync(Guid id);
        Task<EmployeeViewModel> GetEmployeeByMailAsync(string email);
        Task UpdateEmployeeAsync(Guid id, UpdateEmployee command);
    }
}