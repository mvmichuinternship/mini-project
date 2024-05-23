using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.models
{
    public class Order
    {
        public int OId { get; set; }
        public int CustomerId {  get; set; }
        [ForeignKey("CustomerId")]
        public string Status {  get; set; }
        public Customer Customer { get; set; }
    }
}
