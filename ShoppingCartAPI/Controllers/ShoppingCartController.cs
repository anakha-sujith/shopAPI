using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPI.Models;
using ShoppingCartAPI.Repository;

namespace ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingProductRepository shoppingProductRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;

        public ShoppingCartController(IShoppingProductRepository shoppingProductRepository,IShoppingCartRepository shoppingCartRepository) 
        {
            this.shoppingProductRepository = shoppingProductRepository;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        [HttpGet("GetAllProducts")]
        public IActionResult CGetAllProducts()
        {
            var products = shoppingProductRepository.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("GetAnyProduct/{id}")]
        public IActionResult CGetAnyProduct(int id)
        {
            var item = shoppingProductRepository.GetAnyProduct(id);
            if (item != null)
            {
                return Ok(item);
            }
            return NotFound();
        }

        [HttpGet("CartProductById/{uid}")]
        public IActionResult CGetCartProductByUser(int uid)
        {
           var product = shoppingCartRepository.GetCartProductByUser(uid);
           return Ok(product);
        }

        [HttpPost("AddCart")]
        public IActionResult CAddCart(Cart cart)
        {
            shoppingCartRepository.AddCart(cart);
            return Ok(cart);
        }

        [HttpDelete("getCart/{cartId:int}")]
        public IActionResult getCart(int cartId)
        {
            try
            {
                var delete = shoppingCartRepository.getCart(cartId).FirstOrDefault(i => i.CartId == cartId);
                shoppingCartRepository.DeleteCart(delete);
                return Ok(delete);
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    Message ="failed"
                }) ;  
            }
        }
           
        [HttpGet("count/{uid}")]
        public IActionResult CCartCount(int uid)
        {
            var cartcount = shoppingCartRepository.CartCount(uid);
            return Ok(cartcount);
        }
    }
}
