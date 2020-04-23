using System.Text;
using Bank.Infrastructure.Auth;
using Bank.Infrastructure.Database;
using Bank.Infrastructure.Repositories;
using Bank.Extensions;
using Bank.Options;
using Bank.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Bank
{
    public class Startup
    { 
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Database

            services.Configure<SqlOptions>(Configuration.GetSection("SqlDocker"));
            services.AddDbContext<BankContext>();

            #endregion

            #region Jwt

             var jwtSection = Configuration.GetSection("JwtDocker");
             services.Configure<JwtOptions>(jwtSection);
             var jwtOptions = new JwtOptions();
             jwtSection.Bind(jwtOptions);
                        
             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(cfg =>
                 {
                     cfg.TokenValidationParameters = new TokenValidationParameters
                     {
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                         ValidIssuer = jwtOptions.Issuer,
                         ValidateAudience = false,
                         ValidateLifetime = jwtOptions.ValidateLifetime
                     };
                 });
             
             #endregion

            #region Repositories

            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            #endregion

            #region Services

            services.AddScoped<IBankService, BankService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IJwtHandler, JwtHandler>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IAuthService, AuthService>();

            #endregion
            
            services.AddControllers();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseStaticFiles();
            
            app.UseRouting();
            app.UseCors(x => x.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
           
            DatabaseInitializer.PrepPopulation(app).Wait();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseErrorHandler();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}