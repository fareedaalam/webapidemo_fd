using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface IProductInterface
    {
        ProductEntity GetProductById(Guid productId);
        IEnumerable<ProductEntity> GetAllProducts();
        Guid CreateProduct(ProductEntity productEntity);
        bool UpdateProduct(Guid productId, ProductEntity productEntity);
        bool DeleteProduct(Guid productId);
    }
}
