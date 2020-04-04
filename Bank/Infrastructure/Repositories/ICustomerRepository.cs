using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Models;

namespace Bank.Infrastructure.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(Guid id);
        Task<Customer> GetCustomerByMailAsync(string email);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Customer customer);
    }
}