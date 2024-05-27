using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.models
{
    public class Payment
    {
        [Key]
        public int PayId {  get; set; }
        public int OId { get; set; }
        [ForeignKey("OId")]
        public int Amount {  get; set; }
        public string Status {  get; set; }
        public string PaymentMethod { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]

        public Order? Order { get; set; }
    }
}
