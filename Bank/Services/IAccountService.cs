using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Models.Commands;
using Bank.Models.Dtos;

namespace Bank.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDetailsDto>> GetCustomerAccountsAsync(Guid customerId);
        Task<AccountDetailsDto> GetAccountAsync(Guid id);
        Task CreateTransactionAsync(CreateTransaction command);
    }
}