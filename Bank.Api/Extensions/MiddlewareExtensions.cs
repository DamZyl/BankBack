using Bank.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Bank.Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(ErrorHandlerMiddleware));
    }
}