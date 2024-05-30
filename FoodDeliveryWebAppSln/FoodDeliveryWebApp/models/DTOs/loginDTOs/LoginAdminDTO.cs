using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FoodDeliveryWebApp.models.DTOs.loginDTOs
{
    public class LoginAdminDTO
    {
        [Required(ErrorMessage = "User id cannot be empty")]
        [Range(0, 999, ErrorMessage = "Invalid entry for admin ID")]
        [ExcludeFromCodeCoverage]
        public int UserId { get; set; }

        [MinLength(6, ErrorMessage = "Password has to be minmum 6 chars long")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be empty")]
        [ExcludeFromCodeCoverage]
        public string Password { get; set; } = string.Empty;
    }
}
