using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FoodDeliveryWebApp.models.DTOs.registerDTOs
{
    public class RegisterAdminDTO : Admin
    {
        [ExcludeFromCodeCoverage]
        [MinLength(6, ErrorMessage = "Password has to be minmum 6 chars long")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be empty")]
        public string APassword { get; set; }
    }
}
