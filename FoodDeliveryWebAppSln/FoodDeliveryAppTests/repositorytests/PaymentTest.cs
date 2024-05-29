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
    public class PaymentTest
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
            IRepository<int, Payment> repository = new PaymentRepository(context);
            Payment menu = new Payment()
            {
                PayId = 1,
                Status="paid",
                CustomerId=1,
                Amount=60,
                OId=1,
                PaymentMethod="cod"
            };
            var result = await repository.Add(menu);
            Assert.That(result.PayId, Is.EqualTo(1));
        }

        [Test]
        public async Task DelTest()
        {
            IRepository<int, Payment> repository = new PaymentRepository(context);
            Payment menu = new Payment()
            {
                PayId = 2,
                Status = "paid",
                CustomerId = 1,
                Amount = 60,
                OId = 1,
                PaymentMethod = "cod"
            };
            var repo = await repository.Add(menu);
            var result = await repository.Delete(repo.PayId);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTest()
        {
            IRepository<int, Payment> repository = new PaymentRepository(context);

            var menu2 = await repository.Get(1);
            menu2.PaymentMethod = "online";
            var res = await repository.Update(menu2);
            Assert.AreEqual(1, res.PayId);
        }

        [Test]
        public async Task GetTest()
        {
            IRepository<int, Payment> repository = new PaymentRepository(context);
            Payment menu = new Payment()
            {
                PayId = 4,
                Status = "paid",
                CustomerId = 1,
                Amount = 60,
                OId = 1,
                PaymentMethod = "cod"
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.Get(rep.PayId);
            Assert.IsNotNull(result);
        }

        public async Task GetAllTest()
        {
            IRepository<int, Payment> repository = new PaymentRepository(context);
            Payment menu = new Payment()
            {
                PayId = 5,
                Status = "paid",
                CustomerId = 1,
                Amount = 60,
                OId = 1,
                PaymentMethod = "cod"
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.GetAll();
            Assert.That(result, Is.Not.Null);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
