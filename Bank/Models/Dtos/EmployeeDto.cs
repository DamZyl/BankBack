using System;

namespace Bank.Models.Dtos
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
    }
}