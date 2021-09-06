using Bogus;
using System;
using System.Collections.Generic;
using Vavatech.Shop.IServices;
using Vavatech.Shop.Models;
using System.Linq;
using Vavatech.Shop.Models.SearchCritierias;

namespace Vavatech.Shop.FakeServices
{
    public class FakeCustomerService : ICustomerService
    {
        private readonly IEnumerable<Customer> customers;

        public FakeCustomerService(Faker<Customer> faker)
        {
            customers = faker.Generate(100);
        }

        public void Add(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get()
        {
            return customers;
        }

        public Customer Get(int id)
        {
            return customers.SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Customer> Get(string city, string country, string street)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria)
        {
            var query = customers.AsQueryable();

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

        public Customer GetByPesel(string pesel)
        {
            return customers.SingleOrDefault(c => c.Pesel == pesel);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
