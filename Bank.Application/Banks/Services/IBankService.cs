using System;
using System.Threading.Tasks;
using Bank.Application.Auths.Commands;
using Bank.Application.Banks.Commands;
using Bank.Application.Banks.ViewModels;

namespace Bank.Application.Banks.Services
{
    public interface IBankService
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