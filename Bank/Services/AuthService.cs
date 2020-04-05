using System;
using System.Threading.Tasks;
using Bank.Infrastructure.Auth;
using Bank.Infrastructure.Auth.Models;
using Bank.Infrastructure.Repositories;
using Bank.Models.Commands;

namespace Bank.Services
{
    public class AuthService : IAuthService
    {
        private readonly ICustomerRepository _customerRepository;
        //private readonly IEmployeeRepository _employeeRepository; -> ADD LATER!!! ONLY TEST NOW
        private readonly IJwtHandler _jwtHandler;

        public AuthService(ICustomerRepository customerRepository, IJwtHandler jwtHandler)
            //IEmployeeRepository employeeRepository)
        {
            _customerRepository = customerRepository;
            // _employeeRepository = employeeRepository;
            _jwtHandler = jwtHandler;
        }
        
        // LATER!!!
        public Task RegisterAsync(Register command)
        {
            throw new System.NotImplementedException();
        }

        public async Task<TokenDto> LoginAsync(Login command)
        {
            var customer = await _customerRepository.GetCustomerByMailAsync(command.Email);

            if (customer == null)
            {
                throw new Exception("Invalid credentials.");
            }

            if (customer.Password != command.Password)
            {
                throw new Exception("Invalid credentials.");
            }

            var jwt = _jwtHandler.CreateToken(customer.Id, $"{customer.FirstName} {customer.LastName}",
                customer.RoleInSystem.ToString());

            return new TokenDto
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = customer.RoleInSystem.ToString()
            };
        }
        
        // ADD HASH PASSWORD LATER!!!
    }
}