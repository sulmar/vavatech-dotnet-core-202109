using System;
using System.Collections.Generic;
using Vavatech.Shop.Models;
using Vavatech.Shop.Models.SearchCritierias;

namespace Vavatech.Shop.IServices
{
    public interface ICustomerService
    {
        IEnumerable<Customer> Get();
        Customer Get(int id);
        Customer GetByPesel(string pesel);
        // IEnumerable<Customer> Get(string city, string country, string street);

        IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria);

        void Add(Customer customer);
        void Update(Customer customer);
        void Remove(int id);
    }
}
