using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Validators.Abstractions;
using Validators.Polish;

namespace Vavatech.Shop.WebApi.RouteConstraints
{
    public class PeselRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.TryGetValue("pesel", out object peselValue))
            {
                string pesel = peselValue.ToString();


                // dotnet add package PolishValidators
                IValidator validator = new PeselValidator();

                try
                {
                    return validator.IsValid(pesel);
                }
                catch(FormatException)
                {
                    return false;
                }

            }


            return false;
        }
    }
}
