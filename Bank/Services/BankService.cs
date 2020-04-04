using System;
using System.Threading.Tasks;
using Bank.Infrastructure.Mappers;
using Bank.Infrastructure.Repositories;
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

            return Mapper.MapBankToBankDetailsDto(bank);
        }
    }
}