using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.cartDTO;

namespace FoodDeliveryWebApp.interfaces
{
    public interface ICartService
    {
        public Task<CartDetails> AddCartAndDetails(CartAndDetailsDTO cartAndDetailsDTO);

        public Task<Cart> GetCart(int id);
    }
}
