﻿using Bogus;
using System;
using Vavatech.Shop.Models;
using Bogus.Extensions.Poland;
using Microsoft.AspNetCore.Identity;

namespace Vavatech.Shop.Fakers
{
    // dotnet add package Bogus
    // dotnet add package Sulmar.Bogus.Extensions.Poland
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker(Faker<Address> addressFaker, IPasswordHasher<Customer> passwordHasher)
        {
            UseSeed(1);
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.CustomerType, f => f.PickRandom<CustomerType>());
            RuleFor(p => p.Gender, f => (Gender) f.Person.Gender);
            RuleFor(p => p.DateOfBirth, f => f.Person.DateOfBirth);
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f));
            RuleFor(p => p.Pesel, f => f.Person.Pesel());

            RuleFor(p => p.ShipAddress, f => addressFaker.Generate());

            RuleFor(p => p.CreatedOn, f => f.Date.Past(2));

            RuleFor(p => p.Username, f => f.Person.UserName);
            RuleFor(p => p.HashedPassword, (f, customer) => passwordHasher.HashPassword(customer, "12345"));

            // firstname.lastname@domain.com
            RuleFor(p => p.Email, (f, customer) => $"{customer.FirstName}.{customer.LastName}@domain.com");
            
        }
    }

    public class AddressFaker : Faker<Address>
    {
        public AddressFaker()
        {
            RuleFor(p => p.City, f => f.Address.City());
            RuleFor(p => p.Street, f => f.Address.StreetName());
            RuleFor(p => p.ZipCode, f => f.Address.ZipCode());
        }
    }
}
