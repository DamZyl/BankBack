using System;

namespace Bank.Application.Models.ViewModels
{
    public class TransactionViewModel
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public DateTime Date { get; set; }
        public string TransactionType { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}