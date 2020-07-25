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
using BankEntity = Bank.Domain.Models.Bank;


namespace Bank.Application.Services
{
    public class BankService : IBankService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public BankService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<BankDetailsViewModel> GetInfoAsync()
        {
            var bank = await _unitOfWork.Repository<BankEntity>().GetOrFailBankAsync();
            
            return Mapper.MapBankToBankDetailsViewModel(bank);
        }

        public async Task CreateCustomerAsync(CreateCustomer command)
        {
            var customer = await _unitOfWork.Repository<Customer>().GetOrFailCustomerAsync(command.Email);

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

            await _unitOfWork.Repository<Customer>().AddAsync(customer);
            await _unitOfWork.Commit();
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            var customer = await _unitOfWork.Repository<Customer>().GetOrFailCustomerAsync(id);

            await _unitOfWork.Repository<Customer>().DeleteAsync(customer);
            await _unitOfWork.Commit();
        }

        public async Task CreateEmployeeAsync(CreateEmployee command)
        {
            var employee = await _unitOfWork.Repository<Employee>().GetOrFailEmployeeAsync(command.Email);
            
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

            await _unitOfWork.Repository<Employee>().AddAsync(employee);
            await _unitOfWork.Commit();
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            var employee = await _unitOfWork.Repository<Employee>().GetOrFailEmployeeAsync(id);

            await _unitOfWork.Repository<Employee>().DeleteAsync(employee);
            await _unitOfWork.Commit();
        }

        public async Task CreateAccountAsync(CreateAccount command)
        {
            var customer = await _unitOfWork.Repository<Customer>().GetOrFailCustomerAsync(command.CustomerId);
            
            var account = new Account
            {
                Id = command.Id,
                BankId = command.BankId,
                CustomerId = command.CustomerId,
                AccountNumber = command.AccountNumber,
                Balance = command.Balance
            };
            
            customer.Accounts.Add(account);

            await _unitOfWork.Repository<Customer>().EditAsync(customer);
            await _unitOfWork.Commit();
        }

        public async Task DeleteAccountAsync(Guid id)
        {
            var account = await _unitOfWork.Repository<Account>().GetOrFailAccountAsync(id);
            await _unitOfWork.Repository<Account>().DeleteAsync(account);
            await _unitOfWork.Commit();
        }
    }
}