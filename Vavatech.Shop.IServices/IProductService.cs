using Vavatech.Shop.Models;

namespace Vavatech.Shop.IServices
{
    public interface IProductService : IEntityService<Product>
    {
        Product GetByBarCode(string barcode);
    }

    
}
