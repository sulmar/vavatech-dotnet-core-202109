using Bogus;
using Vavatech.Shop.IServices;
using Vavatech.Shop.Models;
using System.Linq;

namespace Vavatech.Shop.FakeServices
{
    public class FakeProductService : FakeEntityService<Product>, IProductService
    {
        public FakeProductService(Faker<Product> faker) : base(faker)
        {
        }

        public Product GetByBarCode(string barcode)
        {
            return entities.SingleOrDefault(p => p.BarCode == barcode);
        }
    }
}
