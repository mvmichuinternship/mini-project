using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebApp.models
{
    public class CartDetails
    {
        public int CartDetailsId {  get; set; }
        public int CartId { get; set; }
        [ForeignKey("CartId")]
        public int FId { get; set; }
        [ForeignKey("FId")]
        public int Qty_ordered { get; set; }
        public int Total { get; set; }
        public Menu Menu { get; set; }
        public Cart Cart { get; set; }
    }
}
