using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);

            if (customer == null)
            {
                throw new Exception("Customer doesn't exist.");
            }

            var accounts = await _accountRepository.GetCustomerAccountsAsync(customer.Id);

            return accounts.Select(Mapper.MapAccountToAccountDetailsDto).ToList();
        }

        public async Task<AccountDetailsDto> GetAccountAsync(Guid id)
        {
            var account = await _accountRepository.GetAccountAsync(id);

            if (account == null)
            {
                throw new Exception("Account doesn't exist.");
            }

            return Mapper.MapAccountToAccountDetailsDto(account);
        }

        public async Task CreateAccountAsync(CreateAccount command)
        {
            var bank = await _bankRepository.GetInfoAsync();

            if (bank == null)
            {
                throw new Exception("Bank doesn't exist.");
            }

            var customer = await _customerRepository.GetCustomerByIdAsync(command.CustomerId);

            if (customer == null)
            {
                throw new Exception("Customer doesn't exist.");
            }

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
            var account = await _accountRepository.GetAccountAsync(id);

            if (account == null)
            {
                throw new Exception("Account doesn't exist.");
            }

            await _accountRepository.DeleteAccountAsync(account);
        }
    }
}