using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Security.Claims;
using Validators.Abstractions;
using Vavatech.Shop.EFDbServices;
using Vavatech.Shop.FakeServices;
using Vavatech.Shop.IServices;
using Vavatech.Shop.Models;
using Vavatech.Shop.Models.Validators;
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
            // services.AddFakeServices();

            services.AddDbServices();

            services.AddSingleton<ICustomerAuthorizationService, CustomerAuthorizationService>();

            // services.AddDbServices();

            // Rejestracja konfiguracji za pomoc¹ IOptions
            services.Configure<FakeOptions>(Configuration.GetSection("FakeOptions"));

            // rejestracja w³asnej regu³y tras
            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("pesel", typeof(PeselRouteConstraint));
            });


            
            services.AddControllers()
                .AddXmlSerializerFormatters()
                .AddNewtonsoftJson()   // dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJsonw celu u¿ycia JsonPatch
                .AddFluentValidation(
                    options => options.RegisterValidatorsFromAssemblyContaining<CustomerValidator>()
                ) // dotnet add package FluentValidation.AspNetCore
                ; 


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vavatech.Shop.WebApi", Version = "v1" });

                var filePath = Path.Combine(AppContext.BaseDirectory, "Vavatech.Shop.WebApi.xml");
                c.IncludeXmlComments(filePath);
            });

            services.AddAuthentication("Basic")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

            services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
            services.AddSingleton<IAuthorizationHandler, GenderHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsWomanAdult", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(ClaimTypes.Gender);
                    // policy.Requirements.Add(new GenderRequirement(Gender.Female));
                    policy.RequireGender(Gender.Female);
                    policy.Requirements.Add(new MinimumAgeRequirement(18));
                });

                options.AddPolicy("IsManAdult", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Gender);
                    policy.Requirements.Add(new GenderRequirement(Gender.Male));
                    policy.Requirements.Add(new MinimumAgeRequirement(21));
                });

                options.AddPolicy("TheSameAuthor", 
                    policy => policy.Requirements.Add(new TheSameAuthorRequriment()));
            });


            services.AddScoped<IClaimsTransformation, CustomerClaimsTransformation>();

            services.AddScoped<IPasswordHasher<Customer>, PasswordHasher<Customer>>();

            // string connectionString = Configuration["ConnectionStrings:ShopConnection"];

            string connectionString = Configuration.GetConnectionString("ShopConnection");

            // dotnet add package Microsoft.EntityFrameworkCore.SqlServer
            // services.AddDbContext<ShopContext>(options => options.UseSqlServer(connectionString));
            services.AddPooledDbContextFactory<ShopContext>(options => options.UseSqlServer(connectionString));

            // services.AddTransient<IValidator<Customer>, CustomerValidator>();

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
