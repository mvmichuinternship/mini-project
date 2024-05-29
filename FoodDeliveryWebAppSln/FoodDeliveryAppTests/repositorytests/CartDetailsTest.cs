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
    public class CartDetailsTest
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
            IRepository<int, CartDetails> repository = new CartDetailsRepository(context);
            CartDetails menu = new CartDetails()
            {
                CartDetailsId = 1,
                CartId = 1,
                FId = 1,
                Qty_ordered= 2,
                Total=60
            };
            var result = await repository.Add(menu);
            Assert.That(result.CartDetailsId, Is.EqualTo(1));
        }

        [Test]
        public async Task DelTest()
        {
            IRepository<int, CartDetails> repository = new CartDetailsRepository(context);
            CartDetails menu = new CartDetails()
            {
                CartDetailsId = 2,
                CartId = 1,
                FId = 1,
                Qty_ordered = 2,
                Total = 60
            };
            var repo = await repository.Add(menu);
            var result = await repository.Delete(repo.CartDetailsId);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTest()
        {
            IRepository<int, CartDetails> repository = new CartDetailsRepository(context);

            
            var repo = await repository.Get(1);
            repo.Total = 60;
            var res = await repository.Update(repo);
            Assert.AreEqual(1, res.CartDetailsId);
        }

        [Test]
        public async Task GetTest()
        {
            IRepository<int, CartDetails> repository = new CartDetailsRepository(context);
            CartDetails menu = new CartDetails()
            {
                CartDetailsId = 14,
                CartId = 1,
                FId = 1,
                Qty_ordered = 2,
                Total = 60
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.Get(rep.CartDetailsId);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllTest()
        {
            IRepository<int, CartDetails> repository = new CartDetailsRepository(context);
            CartDetails menu = new CartDetails()
            {
                CartDetailsId = 4,
                CartId = 1,
                FId = 1,
                Qty_ordered = 2,
                Total = 60
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.GetAll();
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task GetByCIdTest()
        {
            CartDetailsRepository repository = new CartDetailsRepository(context);
            CartDetails menu = new CartDetails()
            {
                CartDetailsId = 6,
                CartId = 1,
                FId = 1,
                Qty_ordered = 2,
                Total = 60
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.GetByCartId(rep.CartId);
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task GetAllByCIdTest()
        {
            CartDetailsRepository repository = new CartDetailsRepository(context);
            CartDetails menu = new CartDetails()
            {
                CartDetailsId = 7,
                CartId = 1,
                FId = 1,
                Qty_ordered = 2,
                Total = 60
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.GetallByCartId(rep.CartId);
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task GetTotalTest()
        {
            CartDetailsRepository repository = new CartDetailsRepository(context);
            CartDetails menu = new CartDetails()
            {
                CartDetailsId = 8,
                CartId = 1,
                FId = 1,
                Qty_ordered = 2,
                Total = 60
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.GetTotalForCart(rep.CartId);
            Assert.IsNotNull(result);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
