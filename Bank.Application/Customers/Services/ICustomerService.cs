using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Application.Customers.Commands;
using Bank.Application.Customers.ViewModels;

namespace Bank.Application.Customers.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDetailsViewModel>> GetCustomersAsync();
        Task<CustomerDetailsViewModel> GetCustomerByIdAsync(Guid id);
        Task<CustomerDetailsViewModel> GetCustomerByMailAsync(string email);
        Task UpdateCustomerAsync(Guid id, UpdateCustomer command);
    }
}