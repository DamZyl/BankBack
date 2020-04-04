using System.Collections.Generic;

namespace Bank.Models.Dtos
{
    public class AccountDetailsDto : AccountDto
    {
        public IEnumerable<TransactionDto> Transactions { get; set; }
    }
}