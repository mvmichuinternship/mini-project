using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.loginDTOs;
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
        private readonly ITokenService _tokenService;

        public RegisterLoginService(IRepository<int, User> userRepo, IRepository<int, Admin> adminRepo, IRepository<int, Customer> customerRepo , ITokenService tokenService)
        {
            _userRepo = userRepo;
            _adminRepo = adminRepo;
            _customerRepo = customerRepo;
            _tokenService = tokenService;
        }

        public async Task<LoginTokenDTO> AdminLogin( LoginAdminDTO loginAdminDTO)
        {
            var userDB = await _userRepo.Get(loginAdminDTO.UserId);
            if (userDB == null)
            {
                throw new UnauthorizedUserException("Invalid username or password");
            }
            HMACSHA512 hMACSHA = new HMACSHA512(userDB.PasswordHashKey);
            var encrypterPass = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(loginAdminDTO.Password));
            bool isPasswordSame = ComparePassword(encrypterPass, userDB.Password);
            if (isPasswordSame)
            {
                var user = await _userRepo.Get(loginAdminDTO.UserId);
                LoginTokenDTO loginReturnDTO = MapEmployeeToLoginReturn(user);
                return loginReturnDTO;
                //if (userDB.Status == "Active")
                //    return employee;
                throw new UserNotActiveException("Your account is not activated");
            }
            throw new UnauthorizedUserException("Invalid username or password");
        }
        private LoginTokenDTO MapEmployeeToLoginReturn(User user)
        {
            LoginTokenDTO returnDTO = new LoginTokenDTO();
            returnDTO.UserID = user.Id;
            returnDTO.Role = user.Role; 
            returnDTO.Token = _tokenService.GenerateToken(user);
            return returnDTO;
        }

        private bool ComparePassword(byte[] encrypterPass, byte[] password)
        {
            for (int i = 0; i < encrypterPass.Length; i++)
            {
                if (encrypterPass[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }

        //public Task<Customer> CustomerLogin(LoginCustomer loginCustomer)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<Admin> AdminRegister(RegisterAdminDTO registerAdminDTO)
        {
            Admin admin = null;
            User user = null;
            try
            {
                admin = registerAdminDTO;
                user = MapAdminUserDTOToUser(registerAdminDTO);
                admin = await _adminRepo.Add(admin);
                //user.Id = admin.Id;
                user = await _userRepo.Add(user);
                ((RegisterAdminDTO)admin).APassword = string.Empty;
                return admin;
            }
            catch (Exception) { }
            if (admin != null)
                await RevertAdminInsert(admin);
            if (user != null && admin == null)
                await RevertUserInsert(user);
            throw new UnableToAddException("Not able to register at this moment");
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
            //user.Id = adminDTO.Id;
            user.Role = "Admin";
            user.Name = adminDTO.Name;
            user.Phone = adminDTO.Phone;
            HMACSHA512 hMACSHA = new HMACSHA512();
            user.PasswordHashKey = hMACSHA.Key;
            user.Password = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(adminDTO.APassword));
            return user;
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
                //user.Id = customer.Id;
                user = await _userRepo.Add(user);
                ((RegisterCustomerDTO)customer).CPassword = string.Empty;
                return customer;
            }
            catch (Exception) { }
            if (customer != null)
                await RevertCustomerInsert(customer);
            if (user != null && customer == null)
                await RevertUserInsert(user);
            throw new UnableToAddException("Not able to register at this moment");
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
            //user.Id = customerDTO.Id;
            user.Role = "Customer";
            user.Name = customerDTO.Name;
            user.Phone = customerDTO.Phone;
            HMACSHA512 hMACSHA = new HMACSHA512();
            user.PasswordHashKey = hMACSHA.Key;
            user.Password = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(customerDTO.CPassword));
            return user;
        }
    }

}
