namespace FoodDeliveryWebApp.repositories.dummymodel
{
    public class OrderTotalResult
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public int Total { get; set; }
    }
}
