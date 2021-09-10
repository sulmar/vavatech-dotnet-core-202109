using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.Shop.Models.Validators
{

    // dotnet add package FluentValidation

    public class CustomerValidator : AbstractValidator<Customer>
    {
        // private readonly ICustomerService customerService;

        public CustomerValidator()
        {
            RuleFor(p => p.LastName).NotEmpty().Length(2, 50).WithName("Nazwisko");
            RuleFor(p => p.Pesel).Length(11).Must(NotExists);
            RuleFor(p => p.Email).EmailAddress();
        }

        private bool NotExists(string pesel)
        {
            return true;
        }
    }

    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {

        }
    }
}
