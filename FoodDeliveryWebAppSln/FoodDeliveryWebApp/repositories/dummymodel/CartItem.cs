using System.Diagnostics.CodeAnalysis;

namespace FoodDeliveryWebApp.repositories.dummymodel
{
    [ExcludeFromCodeCoverage]
    public class CartItem
    {
        public int Fid { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }

    }
}
