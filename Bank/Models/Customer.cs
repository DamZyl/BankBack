using System;
using System.Collections.Generic;

namespace Bank.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<Account> Accounts { get; set; }

        public Customer()
        {
            Accounts = new List<Account>();
        }
    }
}