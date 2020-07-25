using System.Collections.Generic;
using Bank.Application.Accounts.ViewModels;

namespace Bank.Application.Customers.ViewModels
{
    public class CustomerDetailsViewModel : CustomerViewModel
    {
        public IEnumerable<AccountViewModel> Accounts { get; set; }
    }
}