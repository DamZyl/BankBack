using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Application.Accounts.Commands;
using Bank.Application.Accounts.ViewModels;

namespace Bank.Application.Accounts.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDetailsViewModel>> GetCustomerAccountsAsync(Guid customerId);
        Task<AccountDetailsViewModel> GetAccountAsync(Guid id);
        Task CreateTransactionAsync(CreateTransaction command);
    }
}