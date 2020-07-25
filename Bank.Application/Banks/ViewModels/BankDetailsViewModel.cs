using System.Collections.Generic;
using Bank.Application.Accounts.ViewModels;

namespace Bank.Application.Banks.ViewModels
{
    public class BankDetailsViewModel : BankViewModel
    {
        public IEnumerable<AccountViewModel> Accounts { get; set; }
    }
}