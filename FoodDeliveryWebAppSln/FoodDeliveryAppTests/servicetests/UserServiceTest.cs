using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.loginDTOs;
using FoodDeliveryWebApp.models.DTOs.registerDTOs;
using FoodDeliveryWebApp.repositories;
using FoodDeliveryWebApp.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryAppTests.servicetests
{
    public class UserServiceTest
    {
        FoodAppContext context;

        IRepository<int, Admin> adminRepo;
        IRepository<int, Customer> customerRepo;
        IRepository<int, User> userRepo;


        IRegisterLoginService registerService;
        ITokenService tokenService;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("dummyDB");
            context = new FoodAppContext(optionsBuilder.Options);

            adminRepo = new AdminRepository(context);
            registerService = new RegisterLoginService(userRepo, adminRepo,  customerRepo, tokenService);
        }

        [Test]
        public void AddAdminTest()
        {
            //Arrange
            RegisterAdminDTO registerAdmin = new RegisterAdminDTO()
            {
                Id = 1,
                Phone = "1234567899",
                Name = "vk",
                Role = "Admin",
                APassword = "vk1234"
                 };

            //Action
            var result = registerService.AdminRegister(registerAdmin);

            //Assert
            Assert.IsNotNull(result);

        }

        [Test]
        public void AddCustomerTest()
        {
            //Arrange
            RegisterCustomerDTO registerCustomer = new RegisterCustomerDTO()
            {
                Id = 1,
                Phone = "1234567899",
                Name = "vk",
                Role = "Customer",
                CPassword = "vk1234"
            };

            //Action
            var result = registerService.CustomerRegister(registerCustomer);

            //Assert
            Assert.IsNotNull(result);

        }


        [Test]
        public void LoginTest()
        {
            //Arrange
            LoginAdminDTO login = new LoginAdminDTO()
            {
                UserId = 1,
                Password = "vk1234"
            };

            //Action
            var result = registerService.AdminLogin(login);

            //Assert
            Assert.IsNotNull(result);

        }


        [Test]
        public void CreateTokenTest()
        {
            Mock<IConfigurationSection> configurationJWTSection = new Mock<IConfigurationSection>();
            configurationJWTSection.Setup(x => x.Value).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            Mock<IConfigurationSection> congigTokenSection = new Mock<IConfigurationSection>();
            congigTokenSection.Setup(x => x.GetSection("JWT")).Returns(configurationJWTSection.Object);
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x.GetSection("TokenKey")).Returns(congigTokenSection.Object);
            ITokenService service = new TokenService(mockConfig.Object);

            //Action
            var token = service.GenerateToken(new User { Id = 1, Role = "Customer" });

            //Assert
            Assert.IsNotNull(token);

        }
        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
