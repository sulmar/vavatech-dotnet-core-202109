using System;
using System.Collections.Generic;
using Vavatech.Shop.Models;
using Vavatech.Shop.Models.SearchCritierias;

namespace Vavatech.Shop.IServices
{
    public interface ICustomerService : IEntityService<Customer>
    {
        Customer GetByPesel(string pesel);
        // IEnumerable<Customer> Get(string city, string country, string street);

        IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria);

        Customer Get(string username);

    }

    
}
