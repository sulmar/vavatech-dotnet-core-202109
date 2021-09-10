using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.Shop.EFDbServices;

namespace Vavatech.Shop.WebApi
{
    public class ShopContextFactory : IDesignTimeDbContextFactory<ShopContext>
    {
        public ShopContext CreateDbContext(string[] args)
        {
            string connectionString = "Data Source=(local)\\SQLEXPRESS;Integrated Security=True;Initial Catalog=ShopDb;Application Name=Shop";

            var optionsBuilder = new DbContextOptionsBuilder<ShopContext>()
                .UseSqlServer(connectionString);

            var context = new ShopContext(optionsBuilder.Options);

            return context;
                
        }
    }
}
