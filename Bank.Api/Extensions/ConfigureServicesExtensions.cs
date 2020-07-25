using System.Linq;
using System.Reflection;
using System.Text;
using Bank.Application.Services;
using Bank.Domain.Repositories;
using Bank.Infrastructure.Auth;
using Bank.Infrastructure.Repositories;
using Bank.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Bank.Api.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static void AddSqlConfiguration(this IServiceCollection services, IConfiguration configuration, string section)
            => services.Configure<SqlOptions>(x => configuration.GetSection(section).Bind(x));

        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration, string section)
        {
            services.Configure<JwtOptions>(x => configuration.GetSection(section).Bind(x));
            var jwtOption = new JwtOptions();
            var jwtSection = configuration.GetSection(section);
            jwtSection.Bind(jwtOption);
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey)),
                        ValidIssuer = jwtOption.Issuer,
                        ValidateAudience = false,
                        ValidateLifetime = jwtOption.ValidateLifetime
                    };
                });
        }
        
        public static void AddRepositories(this IServiceCollection services)
            => services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        
        public static void AddUnitOfWork(this IServiceCollection services)
            => services.AddScoped<IUnitOfWork, UnitOfWork>();

        public static void AddServices(this IServiceCollection services)
        {
             services.AddScoped<IBankService, BankService>();
             services.AddScoped<ICustomerService, CustomerService>();
             services.AddScoped<IAccountService, AccountService>();
             services.AddScoped<IEmployeeService, EmployeeService>();
             services.AddScoped<IJwtHandler, JwtHandler>();
             services.AddScoped<IPasswordHasher, PasswordHasher>();
             services.AddScoped<IAuthService, AuthService>();
        }
      
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
            });
        }
        
        public static void AddLogger(this IServiceCollection services)
        {
            services.AddLogging(builder => 
            {
                builder.AddSerilog(dispose: true);
            });
        }
    }
}