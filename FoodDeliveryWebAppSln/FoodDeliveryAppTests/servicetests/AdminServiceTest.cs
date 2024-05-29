using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
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
    public class AdminServiceTest
    {
        FoodAppContext context;

        Menu menuItem;

        IRepository<int, Menu> menuRepo;

        IAdminServices adminService;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("dummyDB");
            context = new FoodAppContext(optionsBuilder.Options);

            menuRepo = new MenuRepository(context);
            adminService = new AdminServices(menuRepo);

            menuItem = new Menu() { FId=1, FName = "Dosa", QuantityInStock = 30, UnitPrice=70};

        }


        [Test]

        public async Task AddMenu()
        {
            //Arrange
            menuItem = new Menu() { FId = 2, FName = "Idly", QuantityInStock = 30, UnitPrice = 40 };

            //Action
            var result = await adminService.AddFoodToMenu(menuItem);

            //Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]

        public async Task UpdateMenu()
        {
            //Arrange
            menuItem = new Menu() { FId = 2, FName = "Idly", QuantityInStock = 28, UnitPrice = 40 };

            //Action
            var result = await adminService.UpdateMenu(menuItem);

            //Assert
            Assert.That(result.QuantityInStock,Is.EqualTo(28));
        }


        [Test]

        public async Task DeleteMenu()
        {
            //Arrange
            menuItem = new Menu() { FId = 3, FName = "Roti", QuantityInStock = 28, UnitPrice = 40 };
            var temp = await adminService.AddFoodToMenu(menuItem);
            //Action
            var result = await adminService.DeleteMenu(temp.FId);

            //Assert
            Assert.That(result, Is.Not.Null);
        }


        [Test]

        public async Task GetMenu()
        {
            //Arrange

            //Action
            var result = await adminService.GetAllMenus();

            //Assert
            Assert.That(result, Is.Not.Null);
        }


        [Test]

        public async Task UpdateStockMenu()
        {
            //Arrange
            Menu menuItem1 = new Menu() { FId = 5, FName = "Pulao", QuantityInStock = 28, UnitPrice = 40 };
            var temp = await adminService.AddFoodToMenu(menuItem1);
            //Action
            var result = await adminService.UpdateStock(temp.FId, 3);

            //Assert
            Assert.That(result.QuantityInStock, Is.EqualTo(25));
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
