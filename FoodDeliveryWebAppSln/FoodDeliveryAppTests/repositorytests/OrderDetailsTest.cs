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
    public class OrderDetailsTest
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
            IRepository<int, OrderDetails> repository = new OrderDetailsRepository(context);
            OrderDetails menu = new OrderDetails()
            {
                OrderDetailsId = 1,
                OId = 1,
                FId = 1,
                Qty_ordered = 2,
                Total = 60,
                
            };
            var result = await repository.Add(menu);
            Assert.That(result.OrderDetailsId, Is.EqualTo(1));
        }

        [Test]
        public async Task DelTest()
        {
            IRepository<int, OrderDetails> repository = new OrderDetailsRepository(context);
            OrderDetails menu = new OrderDetails()
            {
                OrderDetailsId = 2,
                OId = 1,
                FId = 1,
                Qty_ordered = 2,
                Total = 60
            };
            var repo = await repository.Add(menu);
            var result = await repository.Delete(repo.OrderDetailsId);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTest()
        {
            IRepository<int, OrderDetails> repository = new OrderDetailsRepository(context);

            var menu2 = await repository.Get(1);
            menu2.Qty_ordered = 2;
            var res = await repository.Update(menu2);
            Assert.AreEqual(1, res.OrderDetailsId);
        }

        [Test]
        public async Task GetTest()
        {
            IRepository<int, OrderDetails> repository = new OrderDetailsRepository(context);
            OrderDetails menu = new OrderDetails()
            {
                OrderDetailsId = 4,
                OId = 1,
                FId = 1,
                Qty_ordered = 2,
                Total = 60
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.Get(rep.OrderDetailsId);
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task GetAllTest()
        {
            IRepository<int, OrderDetails> repository = new OrderDetailsRepository(context);
            OrderDetails menu = new OrderDetails()
            {
                OrderDetailsId = 5,
                OId = 1,
                FId = 1,
                Qty_ordered = 2,
                Total = 60
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.GetAll();
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task GetByOIdAllTest()
        {
            OrderDetailsRepository repository = new OrderDetailsRepository(context);
            OrderDetails menu = new OrderDetails()
            {
                OrderDetailsId = 8,
                OId = 1,
                FId = 1,
                Qty_ordered = 2,
                Total = 60
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.GetByOrderId(rep.OId);
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task GetAllByOIdAllTest()
        {
            OrderDetailsRepository repository = new OrderDetailsRepository(context);
            OrderDetails menu = new OrderDetails()
            {
                OrderDetailsId = 6,
                OId = 1,
                FId = 1,
                Qty_ordered = 2,
                Total = 60
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.GetallByOrderId(rep.OId);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetTotalTest()
        {
            OrderDetailsRepository repository = new OrderDetailsRepository(context);
            OrderDetails menu = new OrderDetails()
            {
                OrderDetailsId = 7,
                OId = 1,
                FId = 1,
                Qty_ordered = 2,
                Total = 60
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.GetTotalForOrder(rep.OId);
            Assert.IsNotNull(result);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
