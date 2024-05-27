using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.cartDTO;
using FoodDeliveryWebApp.repositories.dummymodel;

namespace FoodDeliveryWebApp.interfaces
{
    public interface ICartService
    {
        public Task<CartDetails> AddCartAndDetails(CartAndDetailsDTO cartAndDetailsDTO);

        public Task<Cart> GetCart(int id);
        public Task<CartTotalResult> GetTotal(int id);

        public Task<string> DeleteCart(int id);
    }
}
