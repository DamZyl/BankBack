using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Application.Models.Commands;
using Bank.Application.Models.ViewModels;

namespace Bank.Application.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeViewModel>> GetEmployeesAsync();
        Task<EmployeeViewModel> GetEmployeeByIdAsync(Guid id);
        Task<EmployeeViewModel> GetEmployeeByMailAsync(string email);
        Task UpdateEmployeeAsync(Guid id, UpdateEmployee command);
    }
}