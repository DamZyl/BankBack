using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Application.Extensions;
using Bank.Application.Mappers;
using Bank.Application.Models.Commands;
using Bank.Application.Models.ViewModels;
using Bank.Domain.Repositories;
using Bank.Infrastructure.Exceptions;


namespace Bank.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        
        public async Task<IEnumerable<EmployeeViewModel>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();

            if (employees == null)
            {
                throw new BusinessException(ErrorCodes.NoExist,"No employees");
            }

            return employees.Select(Mapper.MapEmployeeToEmployeeViewModel).ToList();
        }

        public async Task<EmployeeViewModel> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetOrFailAsync(id);

            return Mapper.MapEmployeeToEmployeeViewModel(employee);
        }

        public async Task<EmployeeViewModel> GetEmployeeByMailAsync(string email)
        {
            var employee = await _employeeRepository.GetEmployeeByMailAsync(email);

            if (employee == null)
            {
                throw new BusinessException(ErrorCodes.NoExist,"Employee doesn't exist.");
            }

            return Mapper.MapEmployeeToEmployeeViewModel(employee);
        }

        public async Task UpdateEmployeeAsync(Guid id, UpdateEmployee command)
        {
            var employee = await _employeeRepository.GetOrFailAsync(id);

            employee.Email = command.Email;
            employee.PhoneNumber = command.PhoneNumber;

            await _employeeRepository.UpdateEmployeeAsync(employee);
        }
    }
}