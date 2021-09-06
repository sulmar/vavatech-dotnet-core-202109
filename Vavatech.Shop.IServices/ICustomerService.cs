using System;
using System.Collections.Generic;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.IServices
{
    public interface ICustomerService
    {
        IEnumerable<Customer> Get();
        Customer Get(int id);
        Customer GetByPesel(string pesel);
        IEnumerable<Customer> Get(string city, string country, string street);

        void Add(Customer customer);
        void Update(Customer customer);
        void Remove(int id);
    }
}
