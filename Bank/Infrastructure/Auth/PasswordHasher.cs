using System;
using System.Linq;
using System.Security.Cryptography;

namespace Bank.Infrastructure.Auth
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;
        
        public string Hash(string password)
        {
            using var algorithm = new Rfc2898DeriveBytes(password, SaltSize, Iterations, HashAlgorithmName.SHA256);
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return $"{ Iterations }.{ salt }.{ key }";
        }

        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.', 3);

            if (parts.Length != 3)
            {
                throw new Exception("Invalid credentials.");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using var algorithm = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var keyToCheck = algorithm.GetBytes(KeySize);
            var verified = keyToCheck.SequenceEqual(key);

            return verified;
        }
    }
}