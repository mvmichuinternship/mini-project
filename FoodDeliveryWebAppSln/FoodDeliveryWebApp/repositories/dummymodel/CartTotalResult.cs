using System.Diagnostics.CodeAnalysis;

namespace FoodDeliveryWebApp.repositories.dummymodel
{
    [ExcludeFromCodeCoverage]
    public class CartTotalResult
    {
        public int Id { get; set; }
        public List<CartItem> CartItems { get; set; }
        public int Total { get; set; }
    }

}
