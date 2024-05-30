using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FoodDeliveryWebApp.models.DTOs.orderDTO
{
    public class OrderAndDetailsDTO
    {
        [ExcludeFromCodeCoverage]
        [Required(ErrorMessage = "Cart id cannot be empty")]
        [Range(0, 999, ErrorMessage = "Invalid entry for ID")]
        public int CartId { get; set; }
        //public int Quantity { get; set; }
        [ExcludeFromCodeCoverage]
        [Required(ErrorMessage = "Order id cannot be empty")]
        [Range(0, 999, ErrorMessage = "Invalid entry for ID")]
        public int OId { get; set; }
        //public int CustomerId { get; set; }
    }
}
