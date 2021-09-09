using Bogus;
using System;
using System.Collections.Generic;
using Vavatech.Shop.IServices;
using Vavatech.Shop.Models;
using System.Linq;
using Vavatech.Shop.Models.SearchCritierias;
using Microsoft.Extensions.Options;

namespace Vavatech.Shop.FakeServices
{

    public class FakeCustomerService : FakeEntityService<Customer>, ICustomerService
    {
        public FakeCustomerService(Faker<Customer> faker, IOptions<FakeOptions> options) : base(faker, options)
        {
        }

        private ICollection<Customer> customers => entities;

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

        public override void Update(Customer entity)
        {
            Customer customer = Get(entity.Id);

            customer.FirstName = entity.FirstName;
            customer.LastName = entity.LastName;
        }

        public override void Remove(int id)
        {
            Customer customer = Get(id);
            customer.IsRemoved = true;
        }

        public Customer Get(string username)
        {
            return customers.SingleOrDefault(c => c.Username == username);
        }
    }
}
