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

        public async Task CreateAccountAsync(CreateAccount command)
        {
            var bank = await _bankRepository.GetOrFailAsync();
            var customer = await _customerRepository.GetOrFailAsync(command.CustomerId);

            var account = new Account
            {
                Id = command.Id,
                BankId = command.BankId,
                CustomerId = command.CustomerId,
                AccountNumber = command.AccountNumber,
                Balance = command.Balance
            };

            await _accountRepository.AddAccountAsync(account);
        }

        public async Task DeleteAccountAsync(Guid id)
        {
            var account = await _accountRepository.GetOrFailAsync(id);

            await _accountRepository.DeleteAccountAsync(account);
        }
    }
}