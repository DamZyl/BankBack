using System.Collections.Generic;

namespace Bank.Models.Dtos
{
    public class AccountDetailsDto : AccountDto
    {
        public int TransactionCount { get; set; }
        public IEnumerable<TransactionDto> Transactions { get; set; }
    }
}