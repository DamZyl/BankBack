using System;
using System.Collections.Generic;

namespace Bank.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid BankId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        
        #region EfConfig
        public Bank Bank { get; set; }
        public Customer Customer { get; set; }
        #endregion
        
        public Account()
        {
            Transactions = new List<Transaction>();
        }
    }
}