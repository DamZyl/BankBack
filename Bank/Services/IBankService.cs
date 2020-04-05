using System;
using System.Threading.Tasks;
using Bank.Models.Commands;
using Bank.Models.Dtos;

namespace Bank.Services
{
    public interface IBankService
    {
        Task<BankDetailsDto> GetInfoAsync();
        Task CreateCustomerAsync(CreateCustomer command);
        Task DeleteCustomerAsync(Guid id);
        Task CreateAccountAsync(CreateAccount command);
        Task DeleteAccountAsync(Guid id);
    }
}