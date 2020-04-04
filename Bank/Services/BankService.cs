using System;
using System.Linq;
using System.Threading.Tasks;
using Bank.Infrastructure.Repositories;
using Bank.Models;
using Bank.Models.Dtos;

namespace Bank.Services
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _bankRepository;

        public BankService(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public async Task<BankDetailsDto> GetInfoAsync()
        {
            var bank = await _bankRepository.GetInfoAsync();

            if (bank == null)
            {
                throw new Exception("Bank doesn't exist.");
            }

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

        private static AccountDto MapAccountToAccountDto(Account account)
        {
            return new AccountDto
            {
                Id = account.Id,
                CustomerId = account.CustomerId,
                AccountNumber = account.AccountNumber,
                Balance = account.Balance
            };
        }
    }
}