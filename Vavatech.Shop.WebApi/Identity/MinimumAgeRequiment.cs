using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.WebApi.Identity
{
    public class MinimumAgeRequirement : IAuthorizationRequirement // mark interface
    {
        public int MinimumAge { get; }

        public MinimumAgeRequirement(int minimumAge)
        {
            this.MinimumAge = minimumAge;
        }
    }

    public static class GenderRequirementExtensions
    {
        public static AuthorizationPolicyBuilder RequireGender(this AuthorizationPolicyBuilder policy, Gender gender)
        {
            policy.Requirements.Add(new GenderRequirement(Gender.Female));

            return policy;
        }
    }

    public class GenderRequirement : IAuthorizationRequirement // mark interface
    {
        public Gender Gender { get;  }

        public GenderRequirement(Gender gender)
        {
            this.Gender = gender;
        }
    }

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

    // Note: register services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>(); !
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            if (!context.User.HasClaim(c=>c.Type == ClaimTypes.DateOfBirth))
            {
                context.Fail();

                return Task.CompletedTask;
            }

            DateTime dateOfBirth = Convert.ToDateTime(context.User.FindFirst(ClaimTypes.DateOfBirth).Value);

            int age = DateTime.Today.Year - dateOfBirth.Year;

            if (age >= requirement.MinimumAge)
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
