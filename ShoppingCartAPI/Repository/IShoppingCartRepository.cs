using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Repository
{
    public interface IShoppingCartRepository
    {
        object GetCartProductByUser(int uid);
        void AddCart(Cart cart);
        int CartCount(int uid);
        IEnumerable <Cart> getCart(int cartId);
        void DeleteCart(Cart cart);
    }
}
