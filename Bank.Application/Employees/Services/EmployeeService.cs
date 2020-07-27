using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Application.Employees.Commands;
using Bank.Application.Employees.ViewModels;
using Bank.Application.Extensions;
using Bank.Application.Mappers;
using Bank.Domain.Models;
using Bank.Domain.Repositories;
using Bank.Infrastructure.Exceptions;

namespace Bank.Application.Employees.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<IEnumerable<EmployeeViewModel>> GetEmployeesAsync()
        {
            var employees = await _unitOfWork.Repository<Employee>().GetAllAsync();

            if (employees == null)
            {
                throw new BusinessException(ErrorCodes.NoExist,"No employees");
            }

            return employees.Select(Mapper.MapEmployeeToEmployeeViewModel).ToList();
        }

        public async Task<EmployeeViewModel> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _unitOfWork.Repository<Employee>().GetOrFailAsync(id);

            return Mapper.MapEmployeeToEmployeeViewModel(employee);
        }

        public async Task<EmployeeViewModel> GetEmployeeByMailAsync(string email)
        {
            var employee = await _unitOfWork.Repository<Employee>().FindByAsync(x => x.Email == email);

            if (employee == null)
            {
                throw new BusinessException(ErrorCodes.NoExist,"Employee doesn't exist.");
            }

            return Mapper.MapEmployeeToEmployeeViewModel(employee);
        }

        public async Task UpdateEmployeeAsync(Guid id, UpdateEmployee command)
        {
            var employee = await _unitOfWork.Repository<Employee>().GetOrFailAsync(id);

            employee.Email = command.Email;
            employee.PhoneNumber = command.PhoneNumber;

            await _unitOfWork.Repository<Employee>().EditAsync(employee);
            await _unitOfWork.Commit();
        }
    }
}