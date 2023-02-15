using System.ComponentModel.DataAnnotations;

namespace ShoppingCartAPI.Models
{
    public class CartProduct
    {
       [Key]
        public int CartId { get; set; }
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string ProductType { get; set; }

        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }
    }
}
