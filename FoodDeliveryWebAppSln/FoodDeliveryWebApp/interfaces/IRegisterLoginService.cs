using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.registerDTOs;

namespace FoodDeliveryWebApp.interfaces
{
    public interface IRegisterLoginService
    {
        public Task<Admin> AdminRegister(RegisterAdminDTO registerAdminDTO);
        public Task<Customer> CustomerRegister(RegisterCustomerDTO registerCustomerDTO);

        public Task<Admin> AdminLogin();
        public Task<Customer> CustomerLogin();
    }
}
