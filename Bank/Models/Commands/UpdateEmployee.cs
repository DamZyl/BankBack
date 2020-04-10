namespace Bank.Models.Commands
{
    public class UpdateEmployee
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public UpdateEmployee() { }

        public UpdateEmployee(string phoneNumber, string email)
        {
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}