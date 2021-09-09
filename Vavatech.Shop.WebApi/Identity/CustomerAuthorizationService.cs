using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.Shop.IServices;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.WebApi.Identity
{
    public class CustomerAuthorizationService : ICustomerAuthorizationService
    {
        private readonly ICustomerService customerService;
        private readonly IPasswordHasher<Customer> passwordHasher;

        public CustomerAuthorizationService(ICustomerService customerService, IPasswordHasher<Customer> passwordHasher)
        {
            this.customerService = customerService;
            this.passwordHasher = passwordHasher;
        }

        public bool TryAuthenticate(string username, string password, out Customer customer)
        {
            customer = customerService.Get(username);

            return customer != null 
                && passwordHasher.VerifyHashedPassword(customer, customer.HashedPassword, password) == PasswordVerificationResult.Success;
        }
    }
}
