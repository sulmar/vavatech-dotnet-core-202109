using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using Vavatech.Shop.FakeServices;
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

            // services.AddDbServices();

            // Rejestracja konfiguracji za pomoc� IOptions
            services.Configure<FakeOptions>(Configuration.GetSection("FakeOptions"));

            // rejestracja w�asnej regu�y tras
            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("pesel", typeof(PeselRouteConstraint));
            });


            // dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
            services.AddControllers()
                .AddNewtonsoftJson(); // w celu u�ycia JsonPatch

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vavatech.Shop.WebApi", Version = "v1" });

                var filePath = Path.Combine(AppContext.BaseDirectory, "Vavatech.Shop.WebApi.xml");
                c.IncludeXmlComments(filePath);
            });
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
