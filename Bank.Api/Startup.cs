using Bank.Api.Extensions;
using Bank.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Bank.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .WriteTo.File("")
                .CreateLogger();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSqlConfiguration(Configuration, "SqlLinux");
            services.AddJwtConfiguration(Configuration, "Jwt");
            services.AddDbContext<BankContext>();
            
            services.AddRepositories();
            services.AddUnitOfWork();
            services.AddServices();
            services.AddControllers();
            
            services.AddLogger();
            services.AddSwagger();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DatabaseInitializer.PrepPopulation(app).Wait();
            }
            
            app.UseStaticFiles();
            
            app.UseRouting();
            
            app.UseCors(x => x.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader());
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseErrorHandler();
            app.UseLogger();
            
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
