using System;

namespace Bank.Models.Dtos
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public DateTime Date { get; set; }
        public string TransactionType { get; set; }
        public decimal Value { get; set; }
    }
}