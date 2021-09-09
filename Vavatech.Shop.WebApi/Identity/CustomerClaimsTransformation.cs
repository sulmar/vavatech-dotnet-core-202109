using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vavatech.Shop.IServices;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.WebApi.Identity
{
    public class CustomerClaimsTransformation : IClaimsTransformation
    {
        private readonly ICustomerService customerService;

        public CustomerClaimsTransformation(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ClaimsPrincipal claimsPrincipal = principal.Clone();

            ClaimsIdentity identity = (ClaimsIdentity)claimsPrincipal.Identity;

            string username = identity.FindFirst(ClaimTypes.Name).Value;

            Customer customer = customerService.Get(username);

            identity.AddClaim(new Claim("kat", "A"));
            identity.AddClaim(new Claim("kat", "B"));
            identity.AddClaim(new Claim(ClaimTypes.Email, customer.Email));
            identity.AddClaim(new Claim(ClaimTypes.DateOfBirth, customer.DateOfBirth.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Gender, customer.Gender.ToString()));

            identity.AddClaim(new Claim(ClaimTypes.Role, "Trainer"));

            return Task.FromResult(claimsPrincipal);
        }
    }
}
