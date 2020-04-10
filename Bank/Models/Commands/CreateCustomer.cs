using System;

namespace Bank.Models.Commands
{
    public class CreateCustomer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public CreateCustomer() { }

        public CreateCustomer(string firstName, string lastName, string street, string number, 
            string postCode, string city, string country, string phoneNumber, string email,
            string password)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            Number = number;
            PostCode = postCode;
            City = city;
            Country = country;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
        }
    }
}