using System;
using System.Threading.Tasks;
using Bank.Application.Models.Commands;
using Bank.Application.Models.ViewModels;

namespace Bank.Application.Services
{
    public interface IBankService : IService
    {
        Task<BankDetailsViewModel> GetInfoAsync();
        Task CreateCustomerAsync(CreateCustomer command);
        Task DeleteCustomerAsync(Guid id);
        Task CreateEmployeeAsync(CreateEmployee command);
        Task DeleteEmployeeAsync(Guid id);
        Task CreateAccountAsync(CreateAccount command);
        Task DeleteAccountAsync(Guid id);
    }
}