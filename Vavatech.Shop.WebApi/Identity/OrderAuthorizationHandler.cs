using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.WebApi.Identity
{

    public class TheSameAuthorRequriment : IAuthorizationRequirement
    {

    }

    public class OrderAuthorizationHandler : AuthorizationHandler<TheSameAuthorRequriment, Order>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TheSameAuthorRequriment requirement, Order resource)
        {
            string username = context.User.FindFirstValue(ClaimTypes.Name);

            if (resource.Customer.Username == username)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();

            }

            return Task.CompletedTask;
        }
    }
}
