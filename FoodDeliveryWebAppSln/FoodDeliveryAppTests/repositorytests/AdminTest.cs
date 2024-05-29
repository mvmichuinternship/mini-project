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
  
        public class AdminTest
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
                IRepository<int, Admin> repository = new AdminRepository(context);
                Admin admin = new Admin()
                {
                    Id = 1,
                    Phone = "1234567899",
                    Name = "vk",
                    Role = "Admin",
                };
                var result = await repository.Add(admin);
                Assert.That(result.Id, Is.EqualTo(1));
            }

            [Test]
            public async Task DelTest()
            {
                IRepository<int, Admin> repository = new AdminRepository(context);
                Admin admin = new Admin()
                {
                    Id = 2,
                    Phone = "1234567899",
                    Name = "vk",
                    Role = "Admin",
                };
                var repo = await repository.Add(admin);
                var result = await repository.Delete(repo.Id);
                Assert.IsNotNull(result);
            }

            [Test]
            public async Task UpdateTest()
            {
                IRepository<int, Admin> repository = new AdminRepository(context);

                Admin admin2 = new Admin()
                {
                    Id = 1,
                    Phone = "1234567890",
                    Name = "vk",
                    Role = "Admin",
                };
            var result = await repository.Get(admin2.Id);
            result.Phone = admin2.Phone;
                var res = await repository.Update(result);
                Assert.AreEqual(1, res.Id);
            }

            [Test]
            public async Task GetTest()
            {
                IRepository<int, Admin> repository = new AdminRepository(context);
                Admin admin = new Admin()
                {
                    Id = 4,
                    Phone = "1234567899",
                    Name = "vk",
                    Role = "Admin",
                };

                var rep = await repository.Add(admin);
                var result = await repository.Get(rep.Id);
                Assert.IsNotNull(result);
            }

        [Test]
        public async Task GetAllTest()
        {
            IRepository<int, Admin> repository = new AdminRepository(context);
            Admin admin = new Admin()
            {
                Id = 5,
                Phone = "1234567899",
                Name = "vk",
                Role = "Admin",
            };

            var rep = await repository.Add(admin);
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

