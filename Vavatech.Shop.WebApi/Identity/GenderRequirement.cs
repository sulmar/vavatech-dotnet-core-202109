using Microsoft.AspNetCore.Authorization;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.WebApi.Identity
{
    public class GenderRequirement : IAuthorizationRequirement // mark interface
    {
        public Gender Gender { get;  }

        public GenderRequirement(Gender gender)
        {
            this.Gender = gender;
        }
    }
}
