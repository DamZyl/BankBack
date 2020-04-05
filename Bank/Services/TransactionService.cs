using System;
using System.Threading.Tasks;
using Bank.Extensions;
using Bank.Infrastructure.Repositories;
using Bank.Models;
using Bank.Models.Commands;

namespace Bank.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionService(ITransactionRepository transactionRepository,
            IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }
        
        public async Task CreateTransactionAsync(CreateTransaction command)
        {
            var account = await _accountRepository.GetOrFailAsync(command.AccountId);

            var transaction = new Transaction
            {
                Id = command.Id,
                AccountId = command.AccountId,
                Date = command.Date,
                TransactionType = Enum.Parse<TransactionType>(command.TransactionType),
                Value = command.Value
            };

            await _transactionRepository.AddTransactionAsync(transaction);
        }
    }
}