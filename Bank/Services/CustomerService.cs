using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Infrastructure.Mappers;
using Bank.Infrastructure.Repositories;
using Bank.Models.Commands;
using Bank.Models.Dtos;

namespace Bank.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        
        public async Task<IEnumerable<CustomerDetailsDto>> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetCustomersAsync();

            if (customers == null)
            {
                throw new Exception("No customers.");
            }

            return customers.Select(Mapper.MapCustomerToCustomerDetailsDto).ToList();
        }

        public async Task<CustomerDetailsDto> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                throw new Exception("Customer doesn't exist.");
            }

            return Mapper.MapCustomerToCustomerDetailsDto(customer);
        }

        public async Task<CustomerDetailsDto> GetCustomerByMailAsync(string email)
        {
            var customer = await _customerRepository.GetCustomerByMailAsync(email);

            if (customer == null)
            {
                throw new Exception("Customer doesn't exist.");
            }

            return Mapper.MapCustomerToCustomerDetailsDto(customer);
        }

        public async Task CreateCustomerAsync(CreateCustomer command)
        {
            await Task.CompletedTask;
        }

        public async Task UpdateCustomerAsync(Guid id, UpdateCustomer customer)
        {
            await Task.CompletedTask;
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                throw new Exception("Customer doesn't exist.");
            }

            await _customerRepository.DeleteCustomerAsync(customer);
        }
    }
}