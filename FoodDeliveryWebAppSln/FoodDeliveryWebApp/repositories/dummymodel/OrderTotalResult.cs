using System.Diagnostics.CodeAnalysis;

namespace FoodDeliveryWebApp.repositories.dummymodel
{
    [ExcludeFromCodeCoverage]
    public class OrderTotalResult
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public int Total { get; set; }
    }
}
