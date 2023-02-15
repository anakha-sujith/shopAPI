using ShoppingCartAPI.Controllers;
using ShoppingCartAPI.Data;
using ShoppingCartAPI.Models;
using System.Linq;

namespace ShoppingCartAPI.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShoppingCartDbContext shoppingCartDbContext;

        public ShoppingCartRepository(ShoppingCartDbContext shoppingCartDbContext)
        {
            this.shoppingCartDbContext = shoppingCartDbContext;
                
        }

        public void AddCart(Cart cart)
        {
            shoppingCartDbContext.Cart.Add(cart);
            shoppingCartDbContext.SaveChanges();
        }

        public int CartCount(int uid)
        {
            /*var ProductByUser = (
               from p in shoppingCartDbContext.Product
               join c in shoppingCartDbContext.Cart
               on p.ProductId equals c.ProductId
               where (c.UserId == uid)
               select new
               {
                   ProductId = p.ProductId,
                   ProductName = p.ProductName,
                   ProductType = p.ProductType,
                   Price = p.Price,
                   ProductDescription = p.ProductDescription,
                   ProductImage = p.ProductImage,
                   CartId = c.CartId,
               }).Count();
            shoppingCartDbContext.SaveChanges();
            return (ProductByUser);*/
            var CartCount = from c in shoppingCartDbContext.Cart where (c.UserId == uid) select c;
            return CartCount.Count();
            
        }
       public IEnumerable <Cart> getCart(int cartId)
        {
            return shoppingCartDbContext.Cart.OrderBy(i=>i.CartId).ToList();
        }
        public void DeleteCart(Cart cart)
        {
            shoppingCartDbContext.Cart.Remove(cart);
            shoppingCartDbContext.SaveChanges();
        }

        public object GetCartProductByUser(int uid)
        {
            var ProductByUser = (
               from p in shoppingCartDbContext.Product
               join c in shoppingCartDbContext.Cart
               on p.ProductId equals c.ProductId
               where (c.UserId == uid)
               select new 
               {
                   ProductId = p.ProductId,
                   ProductName = p.ProductName,
                   ProductType = p.ProductType,
                   Price = p.Price,
                   ProductDescription = p.ProductDescription,
                   ProductImage = p.ProductImage,
                   UserId = c.UserId,
                   CartId = c.CartId,
               }).ToList();
            shoppingCartDbContext.SaveChanges();
            return (ProductByUser);
        }
    }
}
