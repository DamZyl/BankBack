using System.Collections.Generic;

namespace Bank.Application.Models.ViewModels
{
    public class BankDetailsViewModel : BankViewModel
    {
        public IEnumerable<AccountViewModel> Accounts { get; set; }
    }
}