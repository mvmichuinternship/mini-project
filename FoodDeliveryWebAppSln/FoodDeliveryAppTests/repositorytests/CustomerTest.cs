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
    public class CustomerTest
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
            IRepository<int, Customer> repository = new CustomerRepository(context);
            Customer customer = new Customer()
            {
                Id = 1,
                Phone = "1234567899",
                Name = "vk",
                Role = "Customer",
               };
            var result = await repository.Add(customer);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test]
        public async Task DelTest()
        {
            IRepository<int, Customer> repository = new CustomerRepository(context);
            Customer customer = new Customer()
            {
                Id = 2,
                Phone = "1234567899",
                Name = "vk",
                Role = "Customer",
               };
            var repo = await repository.Add(customer);
            var result = await repository.Delete(repo.Id);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTest()
        {
            IRepository<int, Customer> repository = new CustomerRepository(context);

            var repo = await repository.Get(1);
            repo.Name = "mk";
            var res = await repository.Update(repo);
            Assert.AreEqual(1, res.Id);
        }

        [Test]
        public async Task GetTest()
        {
            IRepository<int, Customer> repository = new CustomerRepository(context);
            Customer customer = new Customer()
            {
                Id = 4,
                Phone = "1234567899",
                Name = "vk",
                Role = "Customer",
                };

            var rep = await repository.Add(customer);
            var result = await repository.Get(rep.Id);
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task GetAllTest()
        {
            IRepository<int, Customer> repository = new CustomerRepository(context);
            Customer customer = new Customer()
            {
                Id = 5,
                Phone = "1234567899",
                Name = "vk",
                Role = "Customer",
            };

            var rep = await repository.Add(customer);
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
