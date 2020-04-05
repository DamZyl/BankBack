using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Extensions;
using Bank.Infrastructure.Mappers;
using Bank.Infrastructure.Repositories;
using Bank.Models;
using Bank.Models.Commands;
using Bank.Models.Dtos;
using Bank.Models.Enums;

namespace Bank.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBankRepository _bankRepository;

        public AccountService(IAccountRepository accountRepository , ICustomerRepository customerRepository,
            IBankRepository bankRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _bankRepository = bankRepository;
        }
        
        // Delete MAP customer to show all accounts and transactions
        public async Task<IEnumerable<AccountDetailsDto>> GetCustomerAccountsAsync(Guid customerId)
        {
            var customer = await _customerRepository.GetOrFailAsync(customerId);
            var accounts = await _accountRepository.GetCustomerAccountsAsync(customer.Id);

            return accounts.Select(Mapper.MapAccountToAccountDetailsDto).ToList();
        }

        public async Task<AccountDetailsDto> GetAccountAsync(Guid id)
        {
            var account = await _accountRepository.GetOrFailAsync(id);

            return Mapper.MapAccountToAccountDetailsDto(account);
        }
        
        public async Task CreateTransactionAsync(CreateTransaction command)
        {
            var account = await _accountRepository.GetOrFailAsync(command.AccountId);

            var transaction = new Transaction
            {
                Id = command.Id,
                AccountId = command.AccountId,
                Date = command.Date,
                TransactionType = Enum.Parse<TransactionType>(command.TransactionType),
                Description = command.Description,
                Value = command.Value
            };
            
            account.Transactions.Add(transaction);
            account.Balance = account.SumBalance();

            await _accountRepository.UpdateAccountAsync(account);
        }
    }
}