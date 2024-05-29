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
    public class OrderTest
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
            IRepository<int, Order> repository = new OrderRepository(context);
            Order menu = new Order()
            {
                OId = 1,
                CustomerId = 2,
                CartId = 1,
                Status="confirmed"
            };
            var result = await repository.Add(menu);
            Assert.That(result.OId, Is.EqualTo(1));
        }

        [Test]
        public async Task DelTest()
        {
            IRepository<int, Order> repository = new OrderRepository(context);
            Order menu = new Order()
            {
                OId = 2,
                CustomerId = 2,
                CartId = 1,
                Status = "confirmed"
            };
            var repo = await repository.Add(menu);
            var result = await repository.Delete(repo.OId);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTest()
        {
            IRepository<int, Order> repository = new OrderRepository(context);

            var menu2 = await repository.Get(1);
            menu2.CustomerId = 2;
            var res = await repository.Update(menu2);
            Assert.AreEqual(1, res.OId);
        }

        [Test]
        public async Task GetTest()
        {
            IRepository<int, Order> repository = new OrderRepository(context);
            Order menu = new Order()
            {
                OId = 4,
                CustomerId = 2,
                CartId = 1,
                Status = "confirmed"
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.Get(rep.OId);
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task GetAllTest()
        {
            IRepository<int, Order> repository = new OrderRepository(context);
            Order menu = new Order()
            {
                OId = 5,
                CustomerId = 2,
                CartId = 1,
                Status = "confirmed"
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
