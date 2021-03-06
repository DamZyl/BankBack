using Bank.Middlewares.Exceptions;
using Microsoft.AspNetCore.Builder;

namespace Bank.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(ErrorHandlerMiddleware));
    }
}