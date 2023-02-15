using System.ComponentModel.DataAnnotations;

namespace ShoppingCartAPI.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
