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
    public class FbCommentTest
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
            IRepository<int, FbComment> repository = new FeedbackCommentRepository(context);
            FbComment menu = new FbComment()
            {
                CommentId = 1,
                AdminId = 1,
                FbId = 1,

            };
            var result = await repository.Add(menu);
            Assert.That(result.CommentId, Is.EqualTo(1));
        }

        [Test]
        public async Task DelTest()
        {
            IRepository<int, FbComment> repository = new FeedbackCommentRepository(context);
            FbComment menu = new FbComment()
            {
                CommentId = 2,
                AdminId = 1,
                FbId = 1,
            };
            var repo = await repository.Add(menu);
            var result = await repository.Delete(repo.CommentId);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTest()
        {
            IRepository<int, FbComment> repository = new FeedbackCommentRepository(context);

            var repo = await repository.Get(1);
            repo.CommentText = "thanks for fb";
            var res = await repository.Update(repo);
            Assert.AreEqual(1, res.CommentId);
        }

        [Test]
        public async Task GetTest()
        {
            IRepository<int, FbComment> repository = new FeedbackCommentRepository(context);
            FbComment menu = new FbComment()
            {
                CommentId = 4,
                AdminId = 1,
                FbId = 1,
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.Get(rep.CommentId);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllTest()
        {
            IRepository<int, FbComment> repository = new FeedbackCommentRepository(context);
            FbComment menu = new FbComment()
            {
                CommentId = 5,
                AdminId = 1,
                FbId = 1,
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.GetAll();
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task GetByFbIdTest()
        {
            FeedbackCommentRepository repository = new FeedbackCommentRepository(context);

            FbComment menu = new FbComment()
            {
                CommentId = 6,
                AdminId = 1,
                FbId = 1,
            }; ;

            var rep = await repository.Add(menu);
            var result = await repository.GetByFbId(menu.FbId);
            Assert.IsNotNull(result);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
