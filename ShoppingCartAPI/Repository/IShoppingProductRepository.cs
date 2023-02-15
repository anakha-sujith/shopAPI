using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Repository
{
    public interface IShoppingProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetAnyProduct(int id);

    
    }
}
