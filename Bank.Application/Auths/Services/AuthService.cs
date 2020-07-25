using System.Threading.Tasks;
using Bank.Application.Auths.Commands;
using Bank.Application.Extensions;
using Bank.Domain.Models;
using Bank.Domain.Models.Enums;
using Bank.Domain.Repositories;
using Bank.Infrastructure.Auth;
using Bank.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Auths.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtHandler _jwtHandler;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IUnitOfWork unitOfWork, IJwtHandler jwtHandler,
            IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _jwtHandler = jwtHandler;
            _passwordHasher = passwordHasher;
        }
        
        public async Task RegisterAsync(CreateCustomer command)
        {
            var customer = await _unitOfWork.Repository<Customer>().GetOrFailCustomerAsync(command.Email);

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

            await _unitOfWork.Repository<Customer>().AddAsync(customer);
            await _unitOfWork.Commit();
        }

        public async Task<string> LoginAsync(Login command)
        {
            var customer = await _unitOfWork.Repository<Customer>()
                .FindByWithIncludesAsync(x => x.Email == command.Email, 
                includes: i => i.Include(x => x.Accounts));

            if (IsUserValid(customer, command.Password))
            {
                return CreateToken(customer, _jwtHandler);
            }

            var employee = await _unitOfWork.Repository<Employee>().FindByAsync(x => x.Email == command.Email);

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