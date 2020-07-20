using System.Linq;
using Bank.Application.Extensions;
using Bank.Application.Models.ViewModels;
using Bank.Domain.Models;
using BankEntity = Bank.Domain.Models.Bank;

namespace Bank.Application.Mappers
{
    public static class Mapper
    {
        public static BankDetailsViewModel MapBankToBankDetailsViewModel(BankEntity bank)
        {
            return new BankDetailsViewModel
            {
                Id = bank.Id,
                Name = bank.Name,
                Address = $"{ bank.Address.Street } { bank.Address.Number }",
                City = $"{ bank.Address.PostCode } { bank.Address.City }",
                Country = $"{ bank.Address.Country }",
                AccountCount = bank.Accounts.Count,
                Accounts = bank.Accounts.Select(MapAccountToAccountViewModel)
            };
        }

        public static CustomerDetailsViewModel MapCustomerToCustomerDetailsViewModel(Customer customer)
        {
            return new CustomerDetailsViewModel
            {
                Id = customer.Id,
                FullName = $"{ customer.FirstName } { customer.LastName }",
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                Address = $"{ customer.Address.Street } { customer.Address.Number }",
                City = $"{ customer.Address.PostCode } { customer.Address.City }",
                Country = $"{ customer.Address.Country }",
                AccountCount = customer.Accounts.Count,
                Accounts = customer.Accounts.Select(MapAccountToAccountViewModel)
            };
        }

        public static EmployeeViewModel MapEmployeeToEmployeeViewModel(Employee employee)
        {
            return new EmployeeViewModel
            {
                Id = employee.Id,
                FullName = $"{ employee.FirstName } { employee.LastName }",
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Position = employee.Position
            };
        }

        public static AccountDetailsViewModel MapAccountToAccountDetailsViewModel(Account account)
        {
            return new AccountDetailsViewModel
            {
                Id = account.Id,
                CustomerId = account.CustomerId,
                AccountNumber = account.AccountNumber,
                Balance = account.SumBalance(),
                TransactionCount = account.Transactions.Count,
                Transactions = account.Transactions.Select(MapTransactionToTransactionViewModel)
            };
        }

        private static AccountViewModel MapAccountToAccountViewModel(Account account)
        {
            return new AccountViewModel
            {
                Id = account.Id,
                CustomerId = account.CustomerId,
                AccountNumber = account.AccountNumber,
                Balance = account.SumBalance(),
            };
        }

        private static TransactionViewModel MapTransactionToTransactionViewModel(Transaction transaction)
        {
            return new TransactionViewModel
            {
                Id = transaction.Id,
                AccountId = transaction.AccountId,
                Date = transaction.Date,
                TransactionType = transaction.TransactionType.ToString(),
                Description = transaction.Description,
                Value = transaction.Value
            };
        }
    }
}