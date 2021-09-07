using Bogus;
using System;
using Vavatech.Shop.Models;

namespace Vavatech.Shop.Fakers
{
    public class ProductFaker : Faker<Product>
    {
        public ProductFaker()
        {
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Name, f => f.Commerce.ProductName());
            //RuleFor(p => p.UnitPrice, f => decimal.Parse(f.Commerce.Price()));            
            RuleFor(p => p.UnitPrice, f => Math.Round( f.Random.Decimal(1, 100), 2));

            RuleFor(p => p.BarCode, f => f.Commerce.Ean13());
            RuleFor(p => p.Color, f => f.Commerce.Color());
            RuleFor(p => p.Weight, f => f.Random.Float(100, 1000));
        }
    }
}
