using System;

namespace Bank.Application.Models.ViewModels
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }
}