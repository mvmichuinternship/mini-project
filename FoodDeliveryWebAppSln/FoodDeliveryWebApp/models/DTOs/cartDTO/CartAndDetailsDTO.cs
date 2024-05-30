using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FoodDeliveryWebApp.models.DTOs.cartDTO
{
    public class CartAndDetailsDTO : Cart
    {
        [ExcludeFromCodeCoverage]
        [Required(ErrorMessage = "Food id cannot be empty")]
        [Range(0, 999, ErrorMessage = "Invalid entry for ID")]
        public int FId {  get; set; }
        [ExcludeFromCodeCoverage]
        [Required(ErrorMessage = "Quantity id cannot be empty")]
        [Range(0, 999, ErrorMessage = "Invalid entry for quantity")]
        public int Quantity {  get; set; }
    }
}
