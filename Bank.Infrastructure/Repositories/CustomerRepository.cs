using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Infrastructure.Database;
using Bank.Domain.Models;
using Bank.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BankContext _bankContext;

        public CustomerRepository(BankContext bankContext)
        {
            _bankContext = bankContext;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
            => await _bankContext.Customers
                .Include(x => x.Accounts)
                .ToListAsync();

        public async Task<Customer> GetCustomerByIdAsync(Guid id)
            => await _bankContext.Customers
                .Include(x => x.Accounts)
                .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<Customer> GetCustomerByMailAsync(string email)
            => await _bankContext.Customers
                .Include(x => x.Accounts)
                .SingleOrDefaultAsync(x => x.Email == email);

        public async Task AddCustomerAsync(Customer customer)
        {
            await _bankContext.Customers.AddAsync(customer);
            await _bankContext.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _bankContext.Customers.Update(customer);
            await _bankContext.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            _bankContext.Customers.Remove(customer);
            await _bankContext.SaveChangesAsync();
        }
    }
}