namespace Bank.Application.Customers.Commands
{
    public class UpdateCustomer
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public UpdateCustomer() { }

        public UpdateCustomer(string street, string number, string postCode, string city, 
            string country, string phoneNumber, string email)
        {
            Street = street;
            Number = number;
            PostCode = postCode;
            City = city;
            Country = country;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}