using FoodDeliveryWebApp.models.DTOs.cartDTO;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.repositories.dummymodel;
using FoodDeliveryWebApp.models.DTOs.orderDTO;

namespace FoodDeliveryWebApp.interfaces
{
    public interface IOrderService
    {
        public Task<string> AddOrderAndDetails(OrderAndDetailsDTO orderAndDetailsDTO);
        public Task<Order> GetOrder(int id);
        public Task<OrderTotalResult> GetTotal(int id);
    }
}
