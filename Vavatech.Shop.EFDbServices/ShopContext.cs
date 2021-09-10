using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vavatech.Shop.EFDbServices.Configurations;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.EFDbServices
{

    // dotnet add package Microsoft.EntityFrameworkCore
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions options) : base(options)
        {
            // this.Database.EnsureCreated();

            this.Database.Migrate();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent Api

            modelBuilder.Entity<Customer>().OwnsOne(p => p.ShipAddress);

            //modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
     
            // dotnet add package Microsoft.EntityFrameworkCore.Relational
            modelBuilder.Entity<Item>()
                .ToTable("Items");

            modelBuilder.Entity<OrderDetail>()
                .ToTable("OrderDetails");


            base.OnModelCreating(modelBuilder);
        }


    }
}
