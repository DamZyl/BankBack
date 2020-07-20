using System.Threading.Tasks;
using Bank.Application.Extensions;
using Bank.Application.Models.Commands;
using Bank.Domain.Models;
using Bank.Domain.Models.Enums;
using Bank.Domain.Repositories;
using Bank.Infrastructure.Auth;
using Bank.Infrastructure.Exceptions;

namespace Bank.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(ICustomerRepository customerRepository, IJwtHandler jwtHandler,
            IEmployeeRepository employeeRepository, IPasswordHasher passwordHasher)
        {
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _jwtHandler = jwtHandler;
            _passwordHasher = passwordHasher;
        }
        
        public async Task RegisterAsync(CreateCustomer command)
        {
            var customer = await _customerRepository.GetOrFailAsync(command.Email);

            customer = new Customer
            {
                Id = command.Id,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                Address = new Address
                {
                    Street = command.Street,
                    Number = command.Number,
                    PostCode = command.PostCode,
                    City = command.City,
                    Country = command.Country
                },
                Password = _passwordHasher.Hash(command.Password),
                RoleInSystem = RoleType.Customer
            };

            await _customerRepository.AddCustomerAsync(customer);
        }

        public async Task<string> LoginAsync(Login command)
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
            
            throw new BusinessException(ErrorCodes.InvalidCredentials,"Invalid credentials.");
        }

        private bool IsUserValid<T>(T user, string password) where T : User
        {
            return user != null && _passwordHasher.Check(user.Password, password);
        }

        private static string CreateToken<T>(T user, IJwtHandler jwtHandler) where T : User
        {
            var jwt = jwtHandler.CreateToken(user.Id, $"{ user.FirstName } { user.LastName }",
                user.RoleInSystem.ToString());

            /*return new TokenDto
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = user.RoleInSystem.ToString()
            };*/
            return jwt.Token;
        }
    }
}