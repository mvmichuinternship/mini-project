using System.Diagnostics.CodeAnalysis;

namespace FoodDeliveryWebApp.models.DTOs.loginDTOs
{
    public class LoginTokenDTO
    {
        [ExcludeFromCodeCoverage]
        public int UserID { get; set; }
        [ExcludeFromCodeCoverage]
        public string Token { get; set; }
        [ExcludeFromCodeCoverage]
        public string Role { get; set; }
    }
}
