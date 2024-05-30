using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.models.DTOs.cartDTO;
using FoodDeliveryWebApp.models.DTOs.orderDTO;
using FoodDeliveryWebApp.models.DTOs.paymentDTO;
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
    public class CartServiceTest
    {
        FoodAppContext context1;


        IRepository<int, Menu> menuRepo;
        IRepository<int, Cart> cartRepo;
        IRepository<int, CartDetails> cartDetailsRepo;
        IRepository<int, Admin> adminRepo;
        IRepository<int, Customer> customerRepo;
        IRepository<int, User> userRepo;
        IRepository<int, Feedback> feedbackRepo;
        IRepository<int, FbComment> fbCommentRepo;
        IRepository<int, Payment> paymentRepo;
        IRepository<int, Order> orderRepo;
        IRepository<int, OrderDetails> orderDetailsRepo;
        CartDetailsRepository cartDetailsRepository;
        OrderDetailsRepository orderDetailsRepository;
        FeedbackCommentRepository feedbackCommentRepository;

        IRegisterLoginService registerService;
        ICartService cartService;
        ITokenService tokenService;
        IAdminServices adminService;
        IOrderService orderService;
        IFeedbackService feedbackService;
        IPaymentService paymentService;


        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("dummyDB");
            context1 = new FoodAppContext(optionsBuilder.Options);

            cartRepo = new CartRepository(context1);
            menuRepo = new MenuRepository(context1);
            adminRepo = new AdminRepository(context1);
            customerRepo = new CustomerRepository(context1);
            userRepo = new UserRepository(context1);
            feedbackRepo = new FeedbackRepository(context1);
            fbCommentRepo = new FeedbackCommentRepository(context1);
            paymentRepo = new PaymentRepository(context1);
            cartDetailsRepo = new CartDetailsRepository(context1);
            cartDetailsRepository = new CartDetailsRepository(context1);
            orderRepo = new OrderRepository(context1);
            orderDetailsRepo = new OrderDetailsRepository(context1);
            orderDetailsRepository = new OrderDetailsRepository(context1);


            registerService = new RegisterLoginService(userRepo, adminRepo, customerRepo, tokenService);
            cartService = new CartService(cartRepo, cartDetailsRepo, menuRepo, cartDetailsRepository);
            adminService = new AdminServices(menuRepo);
            feedbackService = new FeedbackService(feedbackRepo, fbCommentRepo, feedbackCommentRepository);
            orderService = new OrderService(orderRepo, orderDetailsRepo,menuRepo, orderDetailsRepository,cartDetailsRepository,cartService,adminService,cartRepo,cartDetailsRepo);
            paymentService = new PaymentService(paymentRepo, orderRepo, orderDetailsRepo, orderDetailsRepository);


            Mock<IConfigurationSection> configurationJWTSection = new Mock<IConfigurationSection>();
            configurationJWTSection.Setup(x => x.Value).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            Mock<IConfigurationSection> congigTokenSection = new Mock<IConfigurationSection>();
            congigTokenSection.Setup(x => x.GetSection("JWT")).Returns(configurationJWTSection.Object);
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x.GetSection("TokenKey")).Returns(congigTokenSection.Object);
            tokenService = new TokenService(mockConfig.Object);


            RegisterCustomerDTO registerCustomer = new RegisterCustomerDTO()
            {
                Id = 1,
                Phone = "1234567899",
                Name = "vk",
                Role = "Customer",
                CPassword = "vk1234"
            };
            var cust =  registerService.CustomerRegister(registerCustomer);

            RegisterAdminDTO registerAdmin = new RegisterAdminDTO()
            {
                Id = 1,
                Phone = "1234567899",
                Name = "mv",
                Role = "Admin",
                APassword = "mv1234"
            };
            var adm = registerService.AdminRegister(registerAdmin);

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
            CartAndDetailsDTO cd = new CartAndDetailsDTO()
            {
                CartId = 1,
                CustomerId = 1,
                FId = 1,
                Quantity = 2,
            };


            //Action
            var cart =  cartService.AddCartAndDetails(cd);

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
                CartId = 2,
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

        [Test]
        public async Task AddFeedback()
        {
            Feedback fb = new Feedback()
            {
                FbId = 1,
                Comment="good",
                CustomerId=1,
                FId=1,
                Rating=4
            };

            var result = feedbackService.SendFeedback(fb);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task DeleteFB()
        {

            var result = feedbackService.DeleteFeedback(1);

            Assert.That(result, Is.Not.Null);
        }


        [Test]
        public async Task AddFeedbackComment()
        {
            FbComment fb = new FbComment()
            {
                FbId = 1,
                AdminId = 1,
                CommentId=1,
                CommentText="thanks"
            };

            var result = feedbackService.AddComment(fb);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task DeleteFBComment()
        {

            var result = feedbackService.DeleteComment(1);

            Assert.That(result, Is.Not.Null);
        }


        [Test]
        public async Task AddPayment()
        {
            PaymentDTO pay = new PaymentDTO()
            {
                OId = 1,
                PaymentMethod="cod",
                
            };

            var result = paymentService.AddPayment(pay);

            Assert.That(result, Is.Not.Null);
        }


        [Test]
        public async Task ViewPayment()
        {
           

            var result = paymentService.ViewPayment(1);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task CancelPayment()
        {


            var result = paymentService.CancelPayment(1);

            Assert.That(result, Is.Not.Null);
        }



        [Test]
        public async Task ExceptionTest2()
        {
            FbComment fb = new FbComment()
            {
                FbId = 1,
                AdminId = 1,
                CommentId = 5,
                CommentText = "thanks"
            };
            var exception = Assert.ThrowsAsync<UnableToAddException>(async () => await feedbackService.AddComment(fb));
            //Assert
            Assert.That("Unable to reply to feedback", Is.EqualTo(exception.Message));
        }


        [Test]
        public async Task ExceptionTest3()
        {
            var exception = Assert.ThrowsAsync<UnableToDeleteException>(async () => await feedbackService.DeleteFeedback(100));
            //Assert
            Assert.That("Unable to delete feedback", Is.EqualTo(exception.Message));
        }


        [Test]
        public async Task ExceptionTest4()
        {
            var exception = Assert.ThrowsAsync<UnableToRegisterException>(async () => await orderService.AddOrderAndDetails(new OrderAndDetailsDTO()
            {
                OId = 1,
                CartId = 1,
            }));
            //Assert
            Assert.That(exception.Message, Is.EqualTo("Unable to add to order at the moment"));
        }


        [TearDown]
        public void TearDown()
        {
            context1.Dispose();
        }
    }
}
