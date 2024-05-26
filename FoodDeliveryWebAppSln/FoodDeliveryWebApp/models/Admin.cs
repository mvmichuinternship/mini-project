using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }

        //public int UserId {  get; set; }
        //[ForeignKey("UserId")]
        public string Name { get; set; }
        //public User User { get; set; }
    }
}
