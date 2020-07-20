using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Application.Models.Commands;
using Bank.Application.Models.ViewModels;

namespace Bank.Application.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDetailsViewModel>> GetCustomersAsync();
        Task<CustomerDetailsViewModel> GetCustomerByIdAsync(Guid id);
        Task<CustomerDetailsViewModel> GetCustomerByMailAsync(string email);
        Task UpdateCustomerAsync(Guid id, UpdateCustomer command);
    }
}