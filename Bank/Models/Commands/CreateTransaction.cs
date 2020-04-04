using System;

namespace Bank.Models.Commands
{
    public class CreateTransaction
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public DateTime Date { get; set; }
        public string TransactionType { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }

        public CreateTransaction() { }
        
        public CreateTransaction(Guid accountId, DateTime date, string transactionType,
            string description, decimal value)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            Date = date;
            TransactionType = transactionType;
            Description = description;
            Value = value;
        }
    }
}