using System;
using Bank.Domain.Models.Enums;

namespace Bank.Domain.Models
{
    public abstract class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleType RoleInSystem { get; set; }
    }
}