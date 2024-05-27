using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailsId {  get; set; }
        public int OId { get; set; }
        [ForeignKey("OId")]
        public int FId { get; set; }
        [ForeignKey("FId")]
        public int Qty_ordered { get; set; }
        public int Total { get; set; }
        public Menu? Menu { get; set; }
    }
}
