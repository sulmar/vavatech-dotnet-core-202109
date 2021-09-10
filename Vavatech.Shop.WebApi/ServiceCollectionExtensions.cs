using Bogus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.Shop.EFDbServices;
using Vavatech.Shop.Fakers;
using Vavatech.Shop.FakeServices;
using Vavatech.Shop.IServices;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.WebApi
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, DbCustomerService>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();
            services.AddSingleton<Faker<Address>, AddressFaker>();

            return services;
        }

        public static IServiceCollection AddFakeServices(this IServiceCollection services)
        {
            services.AddFakeCustomerServices();
            services.AddFakerProductServices();

            return services;
        }

        public static IServiceCollection AddFakeCustomerServices(this IServiceCollection services)
        {
            services.AddSingleton<ICustomerService, FakeCustomerService>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();
            services.AddSingleton<Faker<Address>, AddressFaker>();

            return services;
        }

        public static IServiceCollection AddFakerProductServices(this IServiceCollection services)
        {
            services.AddSingleton<IProductService, FakeProductService>();
            services.AddSingleton<Faker<Product>, ProductFaker>();

            return services;
        }
    }
}
