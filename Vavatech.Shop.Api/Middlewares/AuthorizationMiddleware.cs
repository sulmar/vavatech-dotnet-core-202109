using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vavatech.Shop.Api.Middlewares
{
    public static class AuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyAuthorization(this IApplicationBuilder app)
        {
            app.UseMiddleware<AuthorizationMiddleware>();

            return app;
        }
    }

    // od .NET 5
    // Note: zarejestruj w Startup.ConfigureServices()
    // services.AddScoped<AuthorizationMiddleware>();
    public class AuthorizationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                await next(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }
    }
}
