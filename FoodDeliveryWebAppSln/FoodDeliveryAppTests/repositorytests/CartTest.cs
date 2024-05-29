using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryAppTests.repositorytests
{
    public class CartTest
    {
        FoodAppContext context;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                       .UseInMemoryDatabase("dummyDB");
            context = new FoodAppContext(optionsBuilder.Options);

        }

        [Test]
        public async Task AddTest()
        {
            IRepository<int, Cart> repository = new CartRepository(context);
            Cart menu = new Cart()
            {
                CartId = 1,
                CustomerId = 2,
            };
            var result = await repository.Add(menu);
            Assert.That(result.CartId, Is.EqualTo(1));
        }

        [Test]
        public async Task DelTest()
        {
            IRepository<int, Cart> repository = new CartRepository(context);
            Cart menu = new Cart()
            {
                CartId = 2,
                CustomerId = 2,
            };
            var repo = await repository.Add(menu);
            var result = await repository.Delete(repo.CartId);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTest()
        {
            IRepository<int, Cart> repository = new CartRepository(context);

            Cart cart = new Cart()
            {
                CartId = 3,
                CustomerId = 2,
            };
            var addedCart = await repository.Add(cart);

            var cartToUpdate = await repository.Get(addedCart.CartId);

            cartToUpdate.CustomerId = 3;

            var result = await repository.Update(cartToUpdate);

            Assert.AreEqual(3, result.CustomerId);
        }

        [Test]
        public async Task GetTest()
        {
            IRepository<int, Cart> repository = new CartRepository(context);
            Cart menu = new Cart()
            {
                CartId = 14,
                CustomerId = 2,
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.Get(rep.CartId);
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task GetAllTest()
        {
            IRepository<int, Cart> repository = new CartRepository(context);
            Cart menu = new Cart()
            {
                CartId = 4,
                CustomerId = 2,
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.GetAll();
            Assert.IsNotNull(result);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
