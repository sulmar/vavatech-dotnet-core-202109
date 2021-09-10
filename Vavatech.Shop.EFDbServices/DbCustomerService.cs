using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Vavatech.Shop.IServices;
using Vavatech.Shop.Models;
using Vavatech.Shop.Models.SearchCritierias;

namespace Vavatech.Shop.EFDbServices
{
    public class DbCustomerService : ICustomerService
    {
        private readonly ShopContext context;

        private readonly ILogger<DbCustomerService> logger;

        public DbCustomerService(ShopContext context, ILogger<DbCustomerService> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public void Add(Customer entity)
        {

            logger.LogInformation("{0}", context.Entry(entity).State.ToString());

            context.Customers.Add(entity);

            logger.LogInformation("{0}", context.Entry(entity).State.ToString());

            context.SaveChanges();

            logger.LogInformation("{0}", context.Entry(entity).State.ToString());

            entity.IsRemoved = !entity.IsRemoved;

            logger.LogInformation("{0}", context.Entry(entity).State.ToString());
        }

        public IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria)
        {
            var query = context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(searchCriteria.City))
                query = query.Where(c => c.ShipAddress.City == searchCriteria.City);

            if (!string.IsNullOrEmpty(searchCriteria.Street))
                query = query.Where(c => c.ShipAddress.Street == searchCriteria.Street);

            if (searchCriteria.From.HasValue)
                query = query.Where(c => c.DateOfBirth >= searchCriteria.From);

            if (searchCriteria.To.HasValue)
                query = query.Where(c => c.DateOfBirth <= searchCriteria.To);

            return query.ToList();
        }

        public Customer Get(string username)
        {
            return context.Customers.SingleOrDefault(c => c.Username == username);
        }

        public IEnumerable<Customer> Get()
        {
            return context.Customers.ToList();
        }

        public Customer Get(int id)
        {
            return context.Customers.Find(id);
        }

        public Customer GetByPesel(string pesel)
        {
            return context.Customers.SingleOrDefault(p => p.Pesel == pesel);
        }

        public void Remove(int id)
        {
            //Customer customer = Get(id);

            Customer customer = new Customer { Id = id };

            // context.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            context.Customers.Remove(customer);

            context.SaveChanges();
        }

        public void Update(Customer entity)
        {
            context.SaveChanges();
        }
    }
}
