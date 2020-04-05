using System.Linq;
using Bank.Models;
using Bank.Models.Enums;

namespace Bank.Extensions
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