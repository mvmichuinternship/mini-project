using FoodDeliveryWebApp.models;

namespace FoodDeliveryWebApp.interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
