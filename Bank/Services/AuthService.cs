using System;
using System.Threading.Tasks;
using Bank.Infrastructure.Auth;
using Bank.Infrastructure.Auth.Models;
using Bank.Infrastructure.Repositories;
using Bank.Models;
using Bank.Models.Commands;

namespace Bank.Services
{
    public class AuthService : IAuthService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IJwtHandler _jwtHandler;

        public AuthService(ICustomerRepository customerRepository, IJwtHandler jwtHandler,
            IEmployeeRepository employeeRepository)
        {
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
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

            if (IsUserValid(customer, command.Password))
            {
                return CreateToken(customer, _jwtHandler);
            }

            var employee = await _employeeRepository.GetEmployeeByMailAsync(command.Email);

            if (IsUserValid(employee, command.Password))
            {
                return CreateToken(employee, _jwtHandler);
            }
            
            throw new Exception("Invalid credentials.");
        }

        private static bool IsUserValid<T>(T user, string password) where T : User
        {
            if (user == null)
            {
                return false;
            }

            return user.Password == password;
        }

        private static TokenDto CreateToken<T>(T user, IJwtHandler jwtHandler) where T : User
        {
            var jwt = jwtHandler.CreateToken(user.Id, $"{ user.FirstName } { user.LastName }",
                user.RoleInSystem.ToString());

            return new TokenDto
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = user.RoleInSystem.ToString()
            };
        }

        // ADD HASH PASSWORD LATER!!!
    }
}