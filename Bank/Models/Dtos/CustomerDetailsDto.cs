using System.Collections.Generic;

namespace Bank.Models.Dtos
{
    public class CustomerDetailsDto : CustomerDto
    {
        public IEnumerable<AccountDto> Accounts { get; set; }
    }
}