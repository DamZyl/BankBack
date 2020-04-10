using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Infrastructure.Database;
using Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly BankContext _bankContext;

        public EmployeeRepository(BankContext bankContext)
        {
            _bankContext = bankContext;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
            => await _bankContext.Employees.ToListAsync();

        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
            => await _bankContext.Employees.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<Employee> GetEmployeeByMailAsync(string email)
            => await _bankContext.Employees.SingleOrDefaultAsync(x => x.Email == email);

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _bankContext.Employees.AddAsync(employee);
            await _bankContext.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _bankContext.Update(employee);
            await _bankContext.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            _bankContext.Remove(employee);
            await _bankContext.SaveChangesAsync();
        }
    }
}