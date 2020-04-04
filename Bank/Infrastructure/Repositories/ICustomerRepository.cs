using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Models;

namespace Bank.Infrastructure.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerAsync(string email);
        Task AddCustomerAsync(Customer customer);
        Task EditCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Customer customer);
    }
}