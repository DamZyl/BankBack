using System;

namespace Bank.Models.Dtos
{
    public class BankDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int AccountCount { get; set; }
    }
}