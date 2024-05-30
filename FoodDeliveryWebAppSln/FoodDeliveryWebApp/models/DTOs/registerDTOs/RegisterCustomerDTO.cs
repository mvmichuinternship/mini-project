using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryWebApp.models.DTOs.registerDTOs
{
    public class RegisterCustomerDTO : Customer
    {
        [MinLength(6, ErrorMessage = "Password has to be minmum 6 chars long")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be empty")]
        public string CPassword { get; set; }
    }
}
