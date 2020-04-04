using System.Collections.Generic;

namespace Bank.Models.Dtos
{
    public class BankDetailsDto : BankDto
    {
        public IEnumerable<AccountDto> Accounts { get; set; }
    }
}