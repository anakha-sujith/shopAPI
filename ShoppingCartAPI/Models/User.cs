using System.ComponentModel.DataAnnotations;

namespace ShoppingCartAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Passsword { get; set; }
        public string FirstName { get; set; }
        public string  LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Rolee { get; set; }
    }
}
