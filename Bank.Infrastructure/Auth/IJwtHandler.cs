using System;

namespace Bank.Infrastructure.Auth
{
    public interface IJwtHandler
    {
        string CreateToken(Guid userId, string fullName, string role);
    }
}