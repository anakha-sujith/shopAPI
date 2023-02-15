using ShoppingCartAPI.Data;
using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Repository
{
    public class ShoppingProductRepository : IShoppingProductRepository
    {
        private readonly ShoppingCartDbContext shoppingCartDbContext;

        public ShoppingProductRepository(ShoppingCartDbContext shoppingCartDbContext)
        {
            this.shoppingCartDbContext = shoppingCartDbContext;
        }

        public Product GetAnyProduct(int id)
        {
           var item = shoppingCartDbContext.Product.FirstOrDefault(x => x.ProductId == id);
           shoppingCartDbContext.SaveChanges();
            return (item);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return shoppingCartDbContext.Product.ToList();
        }

    }
}
