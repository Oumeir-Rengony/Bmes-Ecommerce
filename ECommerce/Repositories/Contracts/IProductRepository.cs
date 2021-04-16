using System.Collections.Generic;
using ECommerce.Models.Domain.Product;

namespace ECommerce.Repositories.Contracts
{
    public interface IProductRepository
    {
        Product FindProductById(long id);
        IEnumerable<Product> GetAllProducts();
        void SaveProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
