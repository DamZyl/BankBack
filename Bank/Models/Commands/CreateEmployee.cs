using System;

namespace Bank.Models.Commands
{
    public class CreateEmployee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleInSystem { get; set; }
        public string Position { get; set; }

        public CreateEmployee() { }

        public CreateEmployee(string firstName, string lastName, string phoneNumber, string email, 
            string password, string roleInSystem, string position)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
            RoleInSystem = roleInSystem;
            Position = position;
        }
    }
}