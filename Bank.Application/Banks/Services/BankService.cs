using System;
using System.Threading.Tasks;
using Bank.Application.Auths.Commands;
using Bank.Application.Banks.Commands;
using Bank.Application.Banks.ViewModels;
using Bank.Application.Extensions;
using Bank.Application.Mappers;
using Bank.Domain.Models;
using Bank.Domain.Models.Enums;
using Bank.Domain.Repositories;
using Bank.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using BankEntity = Bank.Domain.Models.Bank;


namespace Bank.Application.Banks.Services
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
            var bank = await _unitOfWork.Repository<BankEntity>().GetOrFailWithIncludesAsync(null,
                includes: i => i.Include(x => x.Accounts));
            
            return Mapper.MapBankToBankDetailsViewModel(bank);
        }

        public async Task CreateCustomerAsync(CreateCustomer command)
        {
            await _unitOfWork.Repository<Customer>()
                .GetOrFailWithCheckExistsAsync(x => x.Email == command.Email);

            var customer = new Customer
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
            var customer = await _unitOfWork.Repository<Customer>().GetOrFailAsync(id);

            await _unitOfWork.Repository<Customer>().DeleteAsync(customer);
            await _unitOfWork.Commit();
        }

        public async Task CreateEmployeeAsync(CreateEmployee command)
        {
            await _unitOfWork.Repository<Employee>()
                .GetOrFailWithCheckExistsAsync(x => x.Email == command.Email);
            
            var employee = new Employee
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
            var employee = await _unitOfWork.Repository<Employee>().GetOrFailAsync(id);

            await _unitOfWork.Repository<Employee>().DeleteAsync(employee);
            await _unitOfWork.Commit();
        }

        public async Task CreateAccountAsync(CreateAccount command)
        {
            var customer = await _unitOfWork.Repository<Customer>().GetOrFailAsync(command.CustomerId);
            
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
            var account = await _unitOfWork.Repository<Account>().GetOrFailAsync(id);
            
            await _unitOfWork.Repository<Account>().DeleteAsync(account);
            await _unitOfWork.Commit();
        }
    }
}