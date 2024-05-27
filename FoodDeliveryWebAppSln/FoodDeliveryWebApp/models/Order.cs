using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OId { get; set; }
        public int CartId { get; set; }
        [ForeignKey("CartId")]
        public int CustomerId {  get; set; }
        [ForeignKey("CustomerId")]
        public string Status {  get; set; }
        public Customer? Customer { get; set; }
        public IEnumerable<OrderDetails>? Details { get; set; }
    }
}
