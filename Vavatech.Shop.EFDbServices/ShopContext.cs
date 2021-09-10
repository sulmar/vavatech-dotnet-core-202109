using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.EFDbServices
{

    // dotnet add package Microsoft.EntityFrameworkCore
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }

       



    }
}
