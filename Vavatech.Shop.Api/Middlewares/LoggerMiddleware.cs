using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vavatech.Shop.Api.Middlewares
{
    public static class LoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogger(this IApplicationBuilder app)
        {
            app.UseMiddleware<LoggerMiddleware>();

            return app;
        }
    }

    public class LoggerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<LoggerMiddleware> logger;

        public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger) // <- wymagy parametr w konstruktorze
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            logger.LogInformation($"{context.Request.Method} {context.Request.Path}");

            await next(context);

            logger.LogInformation($"{context.Response.StatusCode}");

        }
    }
}
