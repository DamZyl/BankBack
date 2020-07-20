using System;

namespace Bank.Application.Models.ViewModels
{
    public class BankViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int AccountCount { get; set; }
    }
}