using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.Shop.IServices;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.WebApi.Identity
{
    public class CustomerAuthorizationService : IAuthorizationService
    {
        private readonly ICustomerService customerService;

        public CustomerAuthorizationService(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public bool TryAuthenticate(string username, string password, out Customer customer)
        {
            customer = customerService.Get(username);

            return customer!=null && customer.Password == password;
        }
    }
}
