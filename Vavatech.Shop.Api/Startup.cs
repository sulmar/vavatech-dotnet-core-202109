using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.Shop.Api.Middlewares;
using Vavatech.Shop.Fakers;
using Vavatech.Shop.FakeServices;
using Vavatech.Shop.IServices;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICustomerService, FakeCustomerService>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();
            services.AddSingleton<Faker<Address>, AddressFaker>();

            services.Configure<FakeOptions>(options => options.Count = 20);

            services.AddScoped<AuthorizationMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {

            #region Middlewares

            // Logger Middleware
            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation($"{context.Request.Method} {context.Request.Path}");

            //    await next();

            //    logger.LogInformation($"{context.Response.StatusCode}");
            //});

            // Authorization Middleware
            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Headers.ContainsKey("Authorization"))
            //    {
            //        await next();
            //    }
            //    else
            //    {
            //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //    }
            //});

            #endregion

            // app.UseMiddleware<LoggerMiddleware>();
            // app.UseMiddleware<AuthorizationMiddleware>();

            app.UseLogger();
            app.UseMyAuthorization();

            // Customers Middleware
            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path == "api/customers")
            //    {
            //        await context.Response.WriteAsync("Hello Customers!");
            //    }
            //    else
            //    {
            //        await next();
            //    }
            //});

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("/", async context => await context.Response.WriteAsync("Hello World!"));

                endpoints.MapGet("/api/customers", async context =>
                {
                    ICustomerService customerService = context.RequestServices.GetRequiredService<ICustomerService>();

                    var customers = customerService.Get();

                    await context.Response.WriteAsJsonAsync(customers);

                });

                endpoints.MapGet("api/customers/{id:int}", async context =>
                {
                    int id = Convert.ToInt32( context.Request.RouteValues["id"]);

                    ICustomerService customerService = context.RequestServices.GetRequiredService<ICustomerService>();

                    Customer customer = customerService.Get(id);

                    // await context.Response.WriteAsync($"Hello Customer {id}");

                    await context.Response.WriteAsJsonAsync(customer);
                });

            });

            // Hello Middleware
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
