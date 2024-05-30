using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDeliveryWebApp.services;
using FoodDeliveryWebApp.models.DTOs.registerDTOs;
using FoodDeliveryWebApp.models.DTOs.loginDTOs;

namespace FoodDeliveryAppTests.exceptionstests
{
    public class ExceptionsTest
    {
        FoodAppContext context;

        Menu menuItem;

        IRepository<int, Menu> menuRepo;
        IRepository<int, Admin> adminRepo;
        IRepository<int, Customer> customerRepo;
        IRepository<int, User> userRepo;


        IRegisterLoginService registerService;
        ITokenService tokenService;
        IAdminServices adminService;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("dummyDB");
            context = new FoodAppContext(optionsBuilder.Options);

            menuRepo = new MenuRepository(context);
            adminService = new AdminServices(menuRepo);

            menuItem = new Menu() { FId = 1, FName = "Dosa", QuantityInStock = 30, UnitPrice = 70 };
            adminRepo = new AdminRepository(context);
            userRepo = new UserRepository(context);
            registerService = new RegisterLoginService(userRepo, adminRepo, customerRepo, tokenService);

            adminRepo.Add(new RegisterAdminDTO()
            {
                Id = 1,
                Phone = "1234567899",
                Name = "vk",
                Role = "Admin",
                APassword = "vk1234"
            });

        }

        [Test]
        public async Task ExceptionTest1()
        {
            var exception =  Assert.ThrowsAsync<NoId>(async() => await adminService.DeleteMenu(10));
            //Assert
            Assert.That("No user with the given ID", Is.EqualTo(exception.Message));
        }

        

        

        [Test]
        public async Task ExceptionTest4()
        {
            LoginAdminDTO login = new LoginAdminDTO()
            {
                UserId = 1,
                Password = "vk1234"
            };
            var exception = Assert.ThrowsAsync<UnauthorizedUserException>(async () => await registerService.AdminLogin(login));
            //Assert
            Assert.That(exception.Message, Is.EqualTo("Invalid username or password"));
        }

        

    }
}
