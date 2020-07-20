using System.Linq;
using Bank.Domain.Models;
using Bank.Domain.Models.Enums;

namespace Bank.Application.Extensions
{
    public static class AccountExtension
    {
        public static decimal SumBalance(this Account account)
        {
            decimal balance;
            var transactions = account.Transactions;

            if (transactions == null )
            {
                balance = 0M;
                return balance;
            }

            balance = transactions.Sum(x =>
            {
                if (x.TransactionType == TransactionType.Income)
                {
                    return x.Value;
                }

                return x.Value * (-1);
            });

            return balance;
        }
    }
}