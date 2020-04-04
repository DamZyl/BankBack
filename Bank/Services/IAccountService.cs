using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Models;
using Bank.Models.Commands;
using Bank.Models.Dtos;

namespace Bank.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDetailsDto>> GetCustomerAccountsAsync(Guid customerId);
        Task<AccountDetailsDto> GetAccountAsync(Guid id);
        Task CreateAccountAsync(CreateAccount command);
        Task UpdateAccountAsync(Guid id, UpdateAccount command);
        Task DeleteAccountAsync(Guid id);
    }
}