using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Models.Commands;
using Bank.Models.Dtos;

namespace Bank.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDetailsDto>> GetCustomersAsync();
        Task<CustomerDetailsDto> GetCustomerByIdAsync(Guid id);
        Task<CustomerDetailsDto> GetCustomerByMailAsync(string email);
        Task UpdateCustomerAsync(Guid id, UpdateCustomer command);
    }
}