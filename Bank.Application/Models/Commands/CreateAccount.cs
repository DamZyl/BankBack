using System;

namespace Bank.Application.Models.Commands
{
    public class CreateAccount
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid BankId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }

        public CreateAccount() { }

        public CreateAccount(Guid customerId, Guid bankId, string accountNumber, decimal balance)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            BankId = bankId;
            AccountNumber = accountNumber;
            Balance = balance;
        }
    }
}