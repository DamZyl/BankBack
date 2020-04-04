using System;
using System.Collections.Generic;

namespace Bank.Models
{
    public class Bank
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public ICollection<Account> Accounts { get; set; }

        public Bank()
        {
            Accounts = new List<Account>();
        }
    }
}