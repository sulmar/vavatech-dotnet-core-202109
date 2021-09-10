using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.EFDbServices.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        private readonly Faker<Customer> faker;

        public CustomerConfiguration(Faker<Customer> faker)
        {
            this.faker = faker;
        }

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // builder.OwnsOne(p => p.ShipAddress);
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Pesel).HasMaxLength(11).IsFixedLength().IsUnicode(false);

            var customers = faker.Generate(10);

            builder.HasData(customers);
        }
    }
}
