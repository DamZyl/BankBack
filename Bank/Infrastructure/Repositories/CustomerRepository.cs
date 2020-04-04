using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Models;

namespace Bank.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Customer> GetCustomerAsync(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task AddCustomerAsync(Customer customer)
        {
            throw new System.NotImplementedException();
        }

        public Task EditCustomerAsync(Customer customer)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteCustomerAsync(Customer customer)
        {
            throw new System.NotImplementedException();
        }
    }
}