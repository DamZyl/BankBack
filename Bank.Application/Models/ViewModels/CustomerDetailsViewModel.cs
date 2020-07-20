using System.Collections.Generic;

namespace Bank.Application.Models.ViewModels
{
    public class CustomerDetailsViewModel : CustomerViewModel
    {
        public IEnumerable<AccountViewModel> Accounts { get; set; }
    }
}