using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Extensions;
using Bank.Infrastructure.Mappers;
using Bank.Infrastructure.Repositories;
using Bank.Middlewares.Exceptions;
using Bank.Models;
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
                throw new BusinessException(ErrorCodes.NoExist,"No customers.");
            }

            return customers.Select(Mapper.MapCustomerToCustomerDetailsDto).ToList();
        }

        public async Task<CustomerDetailsDto> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _customerRepository.GetOrFailAsync(id);

            return Mapper.MapCustomerToCustomerDetailsDto(customer);
        }

        public async Task<CustomerDetailsDto> GetCustomerByMailAsync(string email)
        {
            var customer = await _customerRepository.GetCustomerByMailAsync(email);

            if (customer == null)
            {
                throw new BusinessException(ErrorCodes.NoExist,"Customer doesn't exist.");
            }

            return Mapper.MapCustomerToCustomerDetailsDto(customer);
        }

        public async Task UpdateCustomerAsync(Guid id, UpdateCustomer command)
        {
            var customer = await _customerRepository.GetOrFailAsync(id);
            
            customer.Address = new Address
            {
                Street = command.Street,
                Number = command.Number,
                PostCode = command.PostCode,
                City = command.City,
                Country = command.Country
            };

            customer.PhoneNumber = command.PhoneNumber;
            customer.Email = command.Email;

            await _customerRepository.UpdateCustomerAsync(customer);
        }
    }
}