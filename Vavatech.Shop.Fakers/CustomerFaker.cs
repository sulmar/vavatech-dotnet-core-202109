using Bogus;
using System;
using Vavatech.Shop.Models;
using Bogus.Extensions.Poland;

namespace Vavatech.Shop.Fakers
{
    // dotnet add package Bogus
    // dotnet add package Sulmar.Bogus.Extensions.Poland
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker()
        {
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.CustomerType, f => f.PickRandom<CustomerType>());
            RuleFor(p => p.Gender, f => (Gender) f.Person.Gender);
            RuleFor(p => p.DateOfBirth, f => f.Person.DateOfBirth);
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f));
            RuleFor(p => p.Pesel, f => f.Person.Pesel());

            RuleFor(p => p.CreatedOn, f => f.Date.Past(2));

            Ignore(p => p.ShipAddress);
            
        }
    }
}
