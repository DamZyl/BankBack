using System;

namespace Bank.Infrastructure.Auth.Models
{
    public class JwtDto
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}