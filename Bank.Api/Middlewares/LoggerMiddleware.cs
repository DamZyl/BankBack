using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Bank.Api.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<LoggerMiddleware>();
        }
        
        public async Task Invoke(HttpContext context)
        {
            var user = new
                {
                    IsAuthenticated = context.User.Identity.IsAuthenticated,
                    AuthenticationType = context.User.Identity.AuthenticationType,
                    UserName = context.User.Identity.Name,
                    //Claims = context.User.Claims.Select(x => x.Value.ToString())
                };

            _logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = Guid.NewGuid(),
                ["RequestPath"] = context.Request.Path,
                ["RequestMethod"] = context.Request.Method,
                ["RequestQuery"] = context.Request.Query,
                ["RequestContentLength"] = context.Request.ContentLength,
                ["RequestContentType"] = context.Request.ContentType,
                ["User"] = user,
                ["MachineName"] = Environment.MachineName
            });
            
            var sw = new Stopwatch();
            sw.Start();
            
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                sw.Stop();
                
                _logger.BeginScope(new Dictionary<string, object> {
                    ["ElapsedMilliseconds"] = sw.ElapsedMilliseconds 
                });
                
                _logger.LogError(e.Message, "Request Exception");
            }
            finally
            {
                sw.Stop();
                
                _logger.BeginScope(new Dictionary<string, object> {
                    ["ElapsedMilliseconds"] = sw.ElapsedMilliseconds
                });
                
                _logger.LogInformation("Request completed");
            }
        }
    }
}