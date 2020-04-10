using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bank.Infrastructure.Auth.Models;
using Bank.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Bank.Infrastructure.Auth
{
    public class JwtHandler : IJwtHandler
    {
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly SigningCredentials _signingCredentials;
        private readonly TokenValidationParameters _tokenValidationParameters;
        
        public JwtHandler(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions;
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SecretKey));
            _signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
            _tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = issuerSigningKey,
                ValidIssuer = _jwtOptions.Value.Issuer,
                ValidateLifetime = _jwtOptions.Value.ValidateLifetime
            };
        }
        
        public JwtDto CreateToken(Guid userId, string fullName, string role)
        {
            var now = DateTime.Now;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, fullName),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var expires = now.AddMinutes(_jwtOptions.Value.ExpiryMinutes);
            
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Value.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto
            {
                Token = token,
                Expires = expires
            };
        }
    }
}