using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.models
{
    public class Cart
    {
        public int CartId { get; set; }

        public int CustomerId { get; set;}
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set;}

        public IEnumerable<CartDetails> CartDetails { get; set;}
    }
}
