using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Application.Extensions;
using Bank.Application.Mappers;
using Bank.Application.Models.Commands;
using Bank.Application.Models.ViewModels;
using Bank.Domain.Models;
using Bank.Domain.Repositories;
using Bank.Infrastructure.Exceptions;


namespace Bank.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        
        public async Task<IEnumerable<CustomerDetailsViewModel>> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetCustomersAsync();

            if (customers == null)
            {
                throw new BusinessException(ErrorCodes.NoExist,"No customers.");
            }

            return customers.Select(Mapper.MapCustomerToCustomerDetailsViewModel).ToList();
        }

        public async Task<CustomerDetailsViewModel> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _customerRepository.GetOrFailAsync(id);

            return Mapper.MapCustomerToCustomerDetailsViewModel(customer);
        }

        public async Task<CustomerDetailsViewModel> GetCustomerByMailAsync(string email)
        {
            var customer = await _customerRepository.GetCustomerByMailAsync(email);

            if (customer == null)
            {
                throw new BusinessException(ErrorCodes.NoExist,"Customer doesn't exist.");
            }

            return Mapper.MapCustomerToCustomerDetailsViewModel(customer);
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