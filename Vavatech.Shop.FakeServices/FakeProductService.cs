using Bogus;
using Vavatech.Shop.IServices;
using Vavatech.Shop.Models;
using System.Linq;
using Microsoft.Extensions.Options;

namespace Vavatech.Shop.FakeServices
{
    public class FakeProductService : FakeEntityService<Product>, IProductService
    {
        public FakeProductService(Faker<Product> faker, IOptions<FakeOptions> options) : base(faker, options)
        {
        }

        public Product GetByBarCode(string barcode)
        {
            return entities.SingleOrDefault(p => p.BarCode == barcode);
        }
    }
}
