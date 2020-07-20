using System;
using System.Threading.Tasks;
using Bank.Application.Extensions;
using Bank.Application.Mappers;
using Bank.Application.Models.Commands;
using Bank.Application.Models.ViewModels;
using Bank.Domain.Models;
using Bank.Domain.Models.Enums;
using Bank.Domain.Repositories;
using Bank.Infrastructure.Auth;


namespace Bank.Application.Services
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _bankRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;

        public BankService(IBankRepository bankRepository, ICustomerRepository customerRepository,
            IAccountRepository accountRepository, IPasswordHasher passwordHasher, 
            IEmployeeRepository employeeRepository)
        {
            _bankRepository = bankRepository;
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _employeeRepository = employeeRepository;
        }

        public async Task<BankDetailsViewModel> GetInfoAsync()
        {
            var bank = await _bankRepository.GetOrFailAsync();
            
            return Mapper.MapBankToBankDetailsViewModel(bank);
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
                },
                Password = _passwordHasher.Hash(command.Password),
                RoleInSystem = RoleType.Customer
            };

            await _customerRepository.AddCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            var customer = await _customerRepository.GetOrFailAsync(id);

            await _customerRepository.DeleteCustomerAsync(customer);
        }

        public async Task CreateEmployeeAsync(CreateEmployee command)
        {
            var employee = await _employeeRepository.GetOrFailAsync(command.Email);
            
            employee = new Employee
            {
                Id = command.Id,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                Password = _passwordHasher.Hash(command.Password),
                RoleInSystem = Enum.Parse<RoleType>(command.RoleInSystem),
                Position = command.Position
            };

            await _employeeRepository.AddEmployeeAsync(employee);
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            var employee = await _employeeRepository.GetOrFailAsync(id);

            await _employeeRepository.DeleteEmployeeAsync(employee);
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