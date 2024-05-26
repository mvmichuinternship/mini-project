namespace FoodDeliveryWebApp.models.DTOs.loginDTOs
{
    public class LoginTokenDTO
    {
        public int UserID { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
