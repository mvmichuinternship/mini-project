using System.Diagnostics.CodeAnalysis;

namespace FoodDeliveryWebApp.repositories.dummymodel
{
    [ExcludeFromCodeCoverage]
    public class OrderItem
    {
        public int Fid { get; set; }
        public string FName { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
    }
}