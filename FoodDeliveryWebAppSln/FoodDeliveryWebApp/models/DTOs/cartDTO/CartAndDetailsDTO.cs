namespace FoodDeliveryWebApp.models.DTOs.cartDTO
{
    public class CartAndDetailsDTO : Cart
    {
        public int FId {  get; set; }
        public int Quantity {  get; set; }
    }
}
