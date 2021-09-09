using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.WebApi.Identity
{
    public class GenderHandler : AuthorizationHandler<GenderRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GenderRequirement requirement)
        {
            //if (!context.User.HasClaim(c => c.Type == ClaimTypes.Gender))
            //{
            //    context.Fail();

            //    return Task.CompletedTask;
            //}


            Gender gender = (Gender) Enum.Parse(typeof(Gender), context.User.FindFirst(ClaimTypes.Gender).Value);

            if (gender == requirement.Gender)
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
