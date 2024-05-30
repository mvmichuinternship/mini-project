using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FoodDeliveryWebApp.models.DTOs.paymentDTO
{
    public class PaymentDTO
    {
        [ExcludeFromCodeCoverage]
        [Required(ErrorMessage = "Order id cannot be empty")]
        [Range(0, 999, ErrorMessage = "Invalid entry for admin ID")]
        public int OId { get; set; }
        [ExcludeFromCodeCoverage]
        [Required(ErrorMessage = "Payment method cannot be empty", AllowEmptyStrings = false)]
        public string PaymentMethod { get; set; }
    }
}
