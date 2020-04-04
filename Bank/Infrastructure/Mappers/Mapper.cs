using System.Linq;
using Bank.Models;
using Bank.Models.Dtos;
using BankEntity = Bank.Models.Bank;

namespace Bank.Infrastructure.Mappers
{
    public static class Mapper
    {
        public static BankDetailsDto MapBankToBankDetailsDto(BankEntity bank)
        {
            return new BankDetailsDto
            {
                Id = bank.Id,
                Name = bank.Name,
                Address = $"{ bank.Address.Street } { bank.Address.Number }",
                City = $"{ bank.Address.PostCode } { bank.Address.City }",
                Country = $"{ bank.Address.Country }",
                AccountCount = bank.Accounts.Count,
                Accounts = bank.Accounts.Select(MapAccountToAccountDto)
            };
        }

        public static CustomerDetailsDto MapCustomerToCustomerDetailsDto(Customer customer)
        {
            return new CustomerDetailsDto
            {
                Id = customer.Id,
                FullName = $"{ customer.FirstName } { customer.LastName }",
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                Address = $"{ customer.Address.Street } { customer.Address.Number }",
                City = $"{ customer.Address.PostCode } { customer.Address.City }",
                Country = $"{ customer.Address.Country }",
                AccountCount = customer.Accounts.Count,
                Accounts = customer.Accounts.Select(MapAccountToAccountDto)
            };
        }

        public static AccountDetailsDto MapAccountToAccountDetailsDto(Account account)
        {
            return new AccountDetailsDto
            {
                Id = account.Id,
                CustomerId = account.CustomerId,
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                TransactionCount = account.Transactions.Count,
                Transactions = account.Transactions.Select(MapTransactionToTransactionDto)
            };
        }

        private static AccountDto MapAccountToAccountDto(Account account)
        {
            return new AccountDto
            {
                Id = account.Id,
                CustomerId = account.CustomerId,
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
            };
        }

        private static TransactionDto MapTransactionToTransactionDto(Transaction transaction)
        {
            return new TransactionDto
            {
                Id = transaction.Id,
                AccountId = transaction.AccountId,
                Date = transaction.Date,
                TransactionType = transaction.TransactionType.ToString(),
                Value = transaction.Value
            };
        }
    }
}