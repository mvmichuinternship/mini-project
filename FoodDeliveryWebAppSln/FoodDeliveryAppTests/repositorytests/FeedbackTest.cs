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
    public class FeedbackTest
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
            IRepository<int, Feedback> repository = new FeedbackRepository(context);
            Feedback menu2 = new Feedback()
            {
                FbId = 3,
                CustomerId = 1,
                FId = 1,
                Comment = "good",
                Rating = 4,
            };
            
            var result = await repository.Add(menu2);
            Assert.That(result.FbId, Is.EqualTo(3));
        }

        [Test]
        public async Task DelTest()
        {
            IRepository<int, Feedback> repository = new FeedbackRepository(context);
            Feedback menu = new Feedback()
            {
                FbId = 2,
                CustomerId = 1,
                FId = 1,
                Comment = "good",
                Rating = 4,
            };
            var repo = await repository.Add(menu);
            var result = await repository.Delete(repo.FbId);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTest()
        {
            IRepository<int, Feedback> repository = new FeedbackRepository(context);
            var menu2 = await repository.Get(3);
            menu2.Comment = "gooddd";
            var res = await repository.Update(menu2);
            Assert.AreEqual(3, res.FbId);
        }

        [Test]
        public async Task GetTest()
        {
            IRepository<int, Feedback> repository = new FeedbackRepository(context);
            Feedback menu = new Feedback()
            {
                FbId = 4,
                CustomerId = 1,
                FId = 1,
                Comment = "good",
                Rating = 4,
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.Get(rep.FbId);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllTest()
        {
            IRepository<int, Feedback> repository = new FeedbackRepository(context);
            Feedback menu = new Feedback()
            {
                FbId = 5,
                CustomerId = 1,
                FId = 1,
                Comment = "good",
                Rating = 4,
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
