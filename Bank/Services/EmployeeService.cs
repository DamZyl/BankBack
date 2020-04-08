using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Extensions;
using Bank.Infrastructure.Mappers;
using Bank.Infrastructure.Repositories;
using Bank.Middlewares.Exceptions;
using Bank.Models.Commands;
using Bank.Models.Dtos;

namespace Bank.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        
        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();

            if (employees == null)
            {
                throw new BusinessException(ErrorCodes.NoExist,"No employees");
            }

            return employees.Select(Mapper.MapEmployeeToEmployeeDto).ToList();
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetOrFailAsync(id);

            return Mapper.MapEmployeeToEmployeeDto(employee);
        }

        public async Task<EmployeeDto> GetEmployeeByMailAsync(string email)
        {
            var employee = await _employeeRepository.GetEmployeeByMailAsync(email);

            if (employee == null)
            {
                throw new BusinessException(ErrorCodes.NoExist,"Employee doesn't exist.");
            }

            return Mapper.MapEmployeeToEmployeeDto(employee);
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