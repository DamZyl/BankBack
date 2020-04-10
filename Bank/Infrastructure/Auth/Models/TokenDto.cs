using System;

namespace Bank.Infrastructure.Auth.Models
{
    public class TokenDto
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public DateTime Expires { get; set; }
    }
}