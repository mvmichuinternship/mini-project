using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.loginDTOs;
using FoodDeliveryWebApp.models.DTOs.registerDTOs;

namespace FoodDeliveryWebApp.interfaces
{
    public interface IRegisterLoginService
    {
        public Task<Admin> AdminRegister(RegisterAdminDTO registerAdminDTO);
        public Task<Customer> CustomerRegister(RegisterCustomerDTO registerCustomerDTO);

        public Task<LoginTokenDTO> AdminLogin(LoginAdminDTO loginAdminDTO);
        //public Task<Customer> CustomerLogin(LoginCustomer loginCustomer);
    }
}
