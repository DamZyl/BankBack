using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Application.Customers.Commands;
using Bank.Application.Customers.ViewModels;
using Bank.Application.Extensions;
using Bank.Application.Mappers;
using Bank.Domain.Models;
using Bank.Domain.Repositories;
using Bank.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace Bank.Application.Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<IEnumerable<CustomerDetailsViewModel>> GetCustomersAsync()
        {
            var customers = await _unitOfWork.Repository<Customer>()
                .GetAllWithIncludesAsync(includes: i => i.Include(x => x.Accounts));

            if (customers == null)
            {
                throw new BusinessException(ErrorCodes.NoExist,"No customers.");
            }

            return customers.Select(Mapper.MapCustomerToCustomerDetailsViewModel).ToList();
        }

        public async Task<CustomerDetailsViewModel> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _unitOfWork.Repository<Customer>().GetOrFailCustomerAsync(id);

            return Mapper.MapCustomerToCustomerDetailsViewModel(customer);
        }

        public async Task<CustomerDetailsViewModel> GetCustomerByMailAsync(string email)
        {
            var customer = await _unitOfWork.Repository<Customer>()
                .FindByWithIncludesAsync(x => x.Email == email, 
                    includes: i => i.Include(x => x.Accounts));

            if (customer == null)
            {
                throw new BusinessException(ErrorCodes.NoExist,"Customer doesn't exist.");
            }

            return Mapper.MapCustomerToCustomerDetailsViewModel(customer);
        }

        public async Task UpdateCustomerAsync(Guid id, UpdateCustomer command)
        {
            var customer = await _unitOfWork.Repository<Customer>().GetOrFailCustomerAsync(id);
            
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

            await _unitOfWork.Repository<Customer>().EditAsync(customer);
        }
    }
}