using System.Collections.Generic;

namespace Bank.Domain.Models
{
    public class Customer : User
    {
        public Address Address { get; set; }
        public ICollection<Account> Accounts { get; set; }

        public Customer()
        {
            Accounts = new List<Account>();
        }
    }
}