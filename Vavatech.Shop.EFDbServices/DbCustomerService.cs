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

        public DbCustomerService(ShopContext context)
        {
            this.context = context;
        }

        public void Add(Customer entity)
        {
            context.Customers.Add(entity);
            context.SaveChanges();
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
            Customer customer = Get(id);
            context.Customers.Remove(customer);
            context.SaveChanges();
        }

        public void Update(Customer entity)
        {
            context.SaveChanges();
        }
    }
}
