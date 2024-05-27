namespace FoodDeliveryWebApp.repositories.dummymodel
{
    public class CartTotalResult
    {
        public int Id { get; set; }
        public List<CartItem> CartItems { get; set; }
        public int Total { get; set; }
    }

}
