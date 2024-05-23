using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.registerDTOs;
using System.Security.Cryptography;
using System.Text;

namespace FoodDeliveryWebApp.services
{
    public class RegisterLoginService : IRegisterLoginService
    {

        private readonly IRepository<int, User> _userRepo;
        private readonly IRepository<int, Admin> _adminRepo;
        private readonly IRepository<int, Customer> _customerRepo;

        public RegisterLoginService(IRepository<int, User> userRepo, IRepository<int, Admin> adminRepo, IRepository<int, Customer> customerRepo)
        {
            _userRepo = userRepo;
            _adminRepo = adminRepo;
            _customerRepo = customerRepo;
        }

        public Task<Admin> AdminLogin()
        {
            throw new NotImplementedException();
        }

        public async Task<Admin> AdminRegister(RegisterAdminDTO registerAdminDTO)
        {
            Admin admin = null;
            User user = null;
            try
            {
                admin = registerAdminDTO;
                user = MapAdminUserDTOToUser(registerAdminDTO);
                admin = await _adminRepo.Add(admin);
                user.Id = admin.Id;
                user = await _userRepo.Add(user);
                ((RegisterAdminDTO)admin).APassword = string.Empty;
                return admin;
            }
            catch (Exception) { }
            if (admin != null)
                await RevertAdminInsert(admin);
            if (user != null && admin == null)
                await RevertUserInsert(user);
            throw new UnableToRegisterException("Not able to register at this moment");
        }

        private async Task RevertUserInsert(User user)
        {
            await _userRepo.Delete(user.Id);
        }

        private async Task RevertAdminInsert(Admin admin)
        {
            await _adminRepo.Delete(admin.Id);
        }

        private User MapAdminUserDTOToUser(RegisterAdminDTO adminDTO)
        {
            User user = new User();
            user.Id = adminDTO.Id;
            user.Role = "Admin";
            HMACSHA512 hMACSHA = new HMACSHA512();
            user.PasswordHashKey = hMACSHA.Key;
            user.Password = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(adminDTO.APassword));
            return user;
        }

        public Task<Customer> CustomerLogin()
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> CustomerRegister(RegisterCustomerDTO registerCustomerDTO)
        {
            Customer customer = null;
            User user = null;
            try
            {
                customer = registerCustomerDTO;
                user = MapCustomerUserDTOToUser(registerCustomerDTO);
                customer = await _customerRepo.Add(customer);
                user.Id = customer.Id;
                user = await _userRepo.Add(user);
                ((RegisterCustomerDTO)customer).CPassword = string.Empty;
                return customer;
            }
            catch (Exception) { }
            if (customer != null)
                await RevertCustomerInsert(customer);
            if (user != null && customer == null)
                await RevertUserInsert(user);
            throw new UnableToRegisterException("Not able to register at this moment");
        }

        //private async Task RevertUserInsert(User user)
        //{
        //    await _userRepo.Delete(user.CustomerId);
        //}

        private async Task RevertCustomerInsert(Customer customer)
        {
            await _customerRepo.Delete(customer.Id);
        }

        private User MapCustomerUserDTOToUser(RegisterCustomerDTO customerDTO)
        {
            User user = new User();
            user.Id = customerDTO.Id;
            user.Role = "Customer";
            HMACSHA512 hMACSHA = new HMACSHA512();
            user.PasswordHashKey = hMACSHA.Key;
            user.Password = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(customerDTO.CPassword));
            return user;
        }
    }

}
