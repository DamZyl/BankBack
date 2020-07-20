using System;
using Bank.Infrastructure.Auth.Models;

namespace Bank.Infrastructure.Auth
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(Guid userId, string fullName, string role);
    }
}