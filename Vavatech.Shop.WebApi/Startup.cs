using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using Vavatech.Shop.FakeServices;
using Vavatech.Shop.IServices;
using Vavatech.Shop.WebApi.Identity;
using Vavatech.Shop.WebApi.RouteConstraints;

namespace Vavatech.Shop.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             services.AddFakeServices();

            services.AddSingleton<IAuthorizationService, CustomerAuthorizationService>();

            // services.AddDbServices();

            // Rejestracja konfiguracji za pomoc¹ IOptions
            services.Configure<FakeOptions>(Configuration.GetSection("FakeOptions"));

            // rejestracja w³asnej regu³y tras
            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("pesel", typeof(PeselRouteConstraint));
            });


            // dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
            services.AddControllers()
                .AddXmlSerializerFormatters()
                .AddNewtonsoftJson(); // w celu u¿ycia JsonPatch

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vavatech.Shop.WebApi", Version = "v1" });

                var filePath = Path.Combine(AppContext.BaseDirectory, "Vavatech.Shop.WebApi.xml");
                c.IncludeXmlComments(filePath);
            });

            

            services.AddAuthentication("Basic")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string environmentName = env.EnvironmentName;

            //if (env.EnvironmentName== "Testing")
            //{

            //}

            if (env.IsEnvironment("Testing"))
            {

            }


            string nBPApi = Configuration["NBPApiUrl"];
            string code = Configuration["Code"];

            nBPApi = Configuration["NBPApi:Url"];
            code = Configuration["NBPApi:Code"];

            // string connectionString = Configuration["ConnectionStrings:ShopConnection"];

            string connectionString = Configuration.GetConnectionString("ShopConnection");

            string googleMapsKey = Configuration["GoogleMapsKey"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vavatech.Shop.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                if (context.Request.Query.TryGetValue("format", out StringValues values))
                {
                    context.Request.Headers.Add("Content-Type", values[0]);
                }

                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
