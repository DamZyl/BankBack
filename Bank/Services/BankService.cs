using System;
using System.Threading.Tasks;
using Bank.Extensions;
using Bank.Infrastructure.Mappers;
using Bank.Infrastructure.Repositories;
using Bank.Models;
using Bank.Models.Commands;
using Bank.Models.Dtos;

namespace Bank.Services
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _bankRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;

        public BankService(IBankRepository bankRepository, ICustomerRepository customerRepository,
            IAccountRepository accountRepository)
        {
            _bankRepository = bankRepository;
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
        }

        public async Task<BankDetailsDto> GetInfoAsync()
        {
            var bank = await _bankRepository.GetOrFailAsync();
            
            return Mapper.MapBankToBankDetailsDto(bank);
        }

        public async Task CreateCustomerAsync(CreateCustomer command)
        {
            var customer = await _customerRepository.GetOrFailAsync(command.Email);

            customer = new Customer
            {
                Id = command.Id,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                Address = new Address
                {
                    Street = command.Street,
                    Number = command.Number,
                    PostCode = command.PostCode,
                    City = command.City,
                    Country = command.Country
                }
            };

            await _customerRepository.AddCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            var customer = await _customerRepository.GetOrFailAsync(id);

            await _customerRepository.DeleteCustomerAsync(customer);
        }

        public async Task CreateAccountAsync(CreateAccount command)
        {
            var customer = await _customerRepository.GetOrFailAsync(command.CustomerId);
            
            var account = new Account
            {
                Id = command.Id,
                BankId = command.BankId,
                CustomerId = command.CustomerId,
                AccountNumber = command.AccountNumber,
                Balance = command.Balance
            };
            
            customer.Accounts.Add(account);

            await _customerRepository.UpdateCustomerAsync(customer);
        }

        public async Task DeleteAccountAsync(Guid id)
        {
            var account = await _accountRepository.GetOrFailAsync(id);
            await _accountRepository.DeleteAccountAsync(account);
        }
    }
}