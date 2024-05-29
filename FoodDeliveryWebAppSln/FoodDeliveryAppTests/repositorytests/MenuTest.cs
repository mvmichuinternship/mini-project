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
    public class MenuTest
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
            IRepository<int, Menu> repository = new MenuRepository(context);
            Menu menu = new Menu()
            {
                FId = 1,
                FName = "DosaTest",
                QuantityInStock = 10,
                UnitPrice  = 30
            };
            var result = await repository.Add(menu);
            Assert.That(result.FId, Is.EqualTo(1));
        }

        [Test]
        public async Task DelTest()
        {
            IRepository<int, Menu> repository = new MenuRepository(context);
            Menu menu = new Menu()
            {
                FId = 2,
                FName = "IdlyTest",
                QuantityInStock = 10,
                UnitPrice = 30
            };
            var repo = await repository.Add(menu);
            var result = await repository.Delete(repo.FId);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTest()
        {
            IRepository<int, Menu> repository = new MenuRepository(context);

            var menu2 = await repository.Get(1);
            menu2.QuantityInStock = 25;
            var res = await repository.Update(menu2);
            Assert.AreEqual(1, res.FId);
        }

        [Test]
        public async Task GetTest()
        {
            IRepository<int, Menu> repository = new MenuRepository(context);
            Menu menu = new Menu()
            {
                FId = 4,
                FName = "NaanTest",
                QuantityInStock = 10,
                UnitPrice = 30
            };

            var rep = await repository.Add(menu);
            var result = await repository.Get(rep.FId);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllTest()
        {
            IRepository<int, Menu> repository = new MenuRepository(context);
            Menu menu = new Menu()
            {
                FId = 5,
                FName = "NaanTest",
                QuantityInStock = 10,
                UnitPrice = 30
            };

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
