using System;
using Bank.Models.Enums;

namespace Bank.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Description { get; set; } 
        public decimal Value { get; set; }

        #region EfConfig
        public Account Account { get; set; }
        #endregion
        
        public Transaction()
        {
            
        }
    }
}