using Bank.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Bank.Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseErrorHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(ErrorHandlerMiddleware));
    }
}