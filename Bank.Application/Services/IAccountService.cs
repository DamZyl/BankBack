using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Application.Models.Commands;
using Bank.Application.Models.ViewModels;

namespace Bank.Application.Services
{
    public interface IAccountService : IService
    {
        Task<IEnumerable<AccountDetailsViewModel>> GetCustomerAccountsAsync(Guid customerId);
        Task<AccountDetailsViewModel> GetAccountAsync(Guid id);
        Task CreateTransactionAsync(CreateTransaction command);
    }
}