using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Application.Accounts.Commands;
using Bank.Application.Accounts.ViewModels;
using Bank.Application.Extensions;
using Bank.Application.Mappers;
using Bank.Domain.Models;
using Bank.Domain.Models.Enums;
using Bank.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Accounts.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        // Delete MAP customer to show all accounts and transactions
        public async Task<IEnumerable<AccountDetailsViewModel>> GetCustomerAccountsAsync(Guid customerId)
        {
            var customer = await _unitOfWork.Repository<Customer>().GetOrFailAsync(customerId);
            var accounts = await _unitOfWork.Repository<Account>()
                .FindAllWithIncludesAsync(x => x.CustomerId == customerId,
                    includes: i => i.Include(x => x.Transactions));

            return accounts.Select(Mapper.MapAccountToAccountDetailsViewModel).ToList();
        }

        public async Task<AccountDetailsViewModel> GetAccountAsync(Guid id)
        {
            var account = await _unitOfWork.Repository<Account>().GetOrFailAsync(id);

            return Mapper.MapAccountToAccountDetailsViewModel(account);
        }
        
        public async Task CreateTransactionAsync(CreateTransaction command)
        {
            var account = await _unitOfWork.Repository<Account>().GetOrFailAsync(command.AccountId);

            var transaction = new Transaction
            {
                Id = command.Id,
                AccountId = command.AccountId,
                Date = command.Date,
                TransactionType = Enum.Parse<TransactionType>(command.TransactionType),
                Description = command.Description,
                Value = command.Value
            };
            
            account.Transactions.Add(transaction);
            account.Balance = account.SumBalance();

            await _unitOfWork.Repository<Account>().EditAsync(account);
            await _unitOfWork.Commit();
        }
    }
}