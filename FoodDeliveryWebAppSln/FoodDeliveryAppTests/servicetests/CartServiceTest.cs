using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.cartDTO;
using FoodDeliveryWebApp.models.DTOs.orderDTO;
using FoodDeliveryWebApp.models.DTOs.registerDTOs;
using FoodDeliveryWebApp.repositories;
using FoodDeliveryWebApp.services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryAppTests.servicetests
{
    public class CartServiceTest
    {
        FoodAppContext context1;


        IRepository<int, Menu> menuRepo;
        IRepository<int, Cart> cartRepo;
        IRepository<int, CartDetails> cartDetailsRepo;
        IRepository<int, Admin> adminRepo;
        IRepository<int, Customer> customerRepo;
        IRepository<int, User> userRepo;
        IRepository<int, Order> orderRepo;
        IRepository<int, OrderDetails> orderDetailsRepo;
        CartDetailsRepository cartDetailsRepository;
        OrderDetailsRepository orderDetailsRepository;

        IRegisterLoginService registerService;
        ICartService cartService;
        ITokenService tokenService;
        IAdminServices adminService;
        IOrderService orderService;


        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("dummyDB");
            context1 = new FoodAppContext(optionsBuilder.Options);

            cartRepo = new CartRepository(context1);
            menuRepo = new MenuRepository(context1);
            customerRepo = new CustomerRepository(context1);
            userRepo = new UserRepository(context1);
            cartDetailsRepo = new CartDetailsRepository(context1);
            cartDetailsRepository = new CartDetailsRepository(context1);
            orderRepo = new OrderRepository(context1);
            orderDetailsRepo = new OrderDetailsRepository(context1);
            orderDetailsRepository = new OrderDetailsRepository(context1);


            registerService = new RegisterLoginService(userRepo, adminRepo, customerRepo, tokenService);
            cartService = new CartService(cartRepo, cartDetailsRepo, menuRepo, cartDetailsRepository);
            adminService = new AdminServices(menuRepo);
            orderService = new OrderService(orderRepo, orderDetailsRepo,menuRepo, orderDetailsRepository,cartDetailsRepository,cartService,adminService,cartRepo,cartDetailsRepo);



            RegisterCustomerDTO registerCustomer = new RegisterCustomerDTO()
            {
                Id = 1,
                Phone = "1234567899",
                Name = "vk",
                Role = "Customer",
                CPassword = "vk1234"
            };
            var cust =  registerService.CustomerRegister(registerCustomer);

            Menu menuItem = new Menu() { FId = 1, FName = "Dosa", QuantityInStock = 30, UnitPrice = 70 };
            var manu =  adminService.AddFoodToMenu(menuItem);

            //CartAndDetailsDTO cd = new CartAndDetailsDTO()
            //{
            //    CartId = 2,
            //    CustomerId = 1,
            //    FId = 1,
            //    Quantity = 1,
            //};

            //var temp =  cartService.AddCartAndDetails(cd);

            OrderAndDetailsDTO orderAndDetailsDTO = new OrderAndDetailsDTO()
            {
                OId = 2,
                CartId = 1
            };
            var result = orderService.AddOrderAndDetails(orderAndDetailsDTO);

        }

        [Test]
        public async Task AddToCart()
        {
            //Arrange

            CartAndDetailsDTO cd = new CartAndDetailsDTO()
            {
                CartId = 1,
                CustomerId = 1,
                FId = 1,
                Quantity = 2,
            };
                

            //Action
            var result = await cartService.AddCartAndDetails(cd);

            //Assert
            Assert.That(result, Is.Not.Null);

        }

        [Test]
        public async Task GetCart()
        {
            //Arrange


            
            //Action
            var result = await cartService.GetCart(1);

            //Assert
            Assert.That(result.CartId, Is.EqualTo(1));

        }

        [Test]
        public async Task DelCart()
        {
            //Arrange
            CartAndDetailsDTO cd = new CartAndDetailsDTO()
            {
                CartId = 3,
                CustomerId = 1,
                FId = 1,
                Quantity = 1,
            };

            var temp = await cartService.AddCartAndDetails(cd);

            //Action
            var result =  cartService.DeleteCart(1);

            //Assert
            Assert.That(result, Is.Not.Null);

        }

        [Test]
        public async Task GetTotalForCart()
        {

            var result = cartService.GetTotal(1);

            Assert.That(result, Is.Not.Null);
        }


        [Test]
        public async Task AddOrder()
        {
            OrderAndDetailsDTO orderAndDetailsDTO = new OrderAndDetailsDTO() 
            { 
                OId = 1,
                CartId=1
            };
            var result = orderService.AddOrderAndDetails(orderAndDetailsDTO);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task GetOrder()
        {
            
            var result = orderService.GetOrder(1);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task GetTotalForOrder()
        {

            var result = orderService.GetTotal(1);

            Assert.That(result, Is.Not.Null);
        }


        [TearDown]
        public void TearDown()
        {
            context1.Dispose();
        }
    }
}
