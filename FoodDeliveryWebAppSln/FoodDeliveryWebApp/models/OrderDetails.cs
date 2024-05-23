using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.models
{
    public class OrderDetails
    {
        public int OrderDetailsId {  get; set; }
        public int OId { get; set; }
        [ForeignKey("OId")]
        public int FId { get; set; }
        [ForeignKey("FId")]
        public int qty_ordered { get; set; }
        public int total { get; set; }
        public Menu Menu { get; set; }
        public Order Order { get; set; }
    }
}
