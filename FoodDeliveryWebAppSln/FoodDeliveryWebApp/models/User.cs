using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryWebApp.models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Role { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordHashKey { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }

        public Customer? Customer { get; set; }
        public Admin? Admin { get; set; }
    }
}
