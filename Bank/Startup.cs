using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Infrastructure.Database;
using Bank.Infrastructure.Repositories;
using Bank.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
            var connectionString = Configuration.GetSection("SqlLinux")["ConnectionString"];

            services.AddDbContext<BankContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<DatabaseInitializer>();
            
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IBankService, BankService>();
            
            services.AddControllers();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseInitializer initializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            initializer.SeedData().Wait();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}