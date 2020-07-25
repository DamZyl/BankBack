using System.Collections.Generic;

namespace Bank.Application.Accounts.ViewModels
{
    public class AccountDetailsViewModel : AccountViewModel
    {
        public int TransactionCount { get; set; }
        public IEnumerable<TransactionViewModel> Transactions { get; set; }
    }
}