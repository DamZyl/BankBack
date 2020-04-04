using System;

namespace Bank.Models.Dtos
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }
}