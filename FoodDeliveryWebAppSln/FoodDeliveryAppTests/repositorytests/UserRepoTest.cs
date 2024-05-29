using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.repositories;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace FoodDeliveryAppTests.repositorytests
{
    public class Tests
    {
        FoodAppContext context;

        //IRepository<int, User> repository;
        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                       .UseInMemoryDatabase("dummyDB");
            context = new FoodAppContext(optionsBuilder.Options);
            //repository = new UserRepository(context);


           // User customer = new User() { Id = 1, Phone = "1234567890", Name="mv", Role = "Admin", 
           //Password= Encoding.ASCII.GetBytes("0x5AFEE86D93BAEB7AAD7DC20DC9B4A1B5ED8B8DED41BA1C15AFF869F48042295B6539EE8495257A3DE7CBCF4B063942E35AB0147C5309D265F48E96B864075C27"), 
           //PasswordHashKey= Encoding.ASCII.GetBytes("0xB5BC90D0D28B1B7301CEA29984FE58C5F17054189127489006B764ABDD35168A1F9F7FFDF3015431DEB092EC54F9CD64F6F5735DF3394DE9EF2FEB122CE43E8C7A7FBF570DE44F73C307856ED87B93E69D85D623E845B12F1019B896E57DEA7C7A6E0C84CC77490BE12F32FF5A87F3B7C0A00C6A424A06A295806F11294E6C6B") };
           // var result = repository.Add(customer);
        }

        [Test]
        public async Task AddTest()
        {
            IRepository<int, User> repository = new UserRepository(context);
            User customer = new User() { Id = 1,  Phone = "1234567899", Name="vk", Role="Customer", 
            Password= Encoding.ASCII.GetBytes("0xF2AFD30723ADD33EA4185F0E8DC0581D3E12897EFFFE775FD1172CF21CD72CC7888BC8D7E6D6463CBEA3BBEDF6FFD007F291B159452265EFAE46B210D3B6682A"),
            PasswordHashKey = Encoding.ASCII.GetBytes("0x183DA481D881458F417E33778DF0A99A9FF8123BCB4A3EE9087CFC2AB2FE4BB2013B7EDE791CBEE161E36BBC88D38B8789C343A71FFCE942BF595E649D945ACA500B7AA72CA0D93F4E6B20BA13EEA5D26FF44322FD0EBA445DA7E33419B0838C274ACC4CC15291B56EC95A7493A1ECC6B8094DE6A2C335ED6A71078B16F7ECA0")
            };
            var result = await repository.Add(customer);
            Assert.That(result.Id,Is.EqualTo(1));
        }

        [Test]
        public async Task DelTest()
        {
            IRepository<int, User> repository = new UserRepository(context);
            User customer = new User()
            {
                Id = 2,
                Phone = "1234567899",
                Name = "vk",
                Role = "Customer",
                Password = Encoding.ASCII.GetBytes("0xF2AFD30723ADD33EA4185F0E8DC0581D3E12897EFFFE775FD1172CF21CD72CC7888BC8D7E6D6463CBEA3BBEDF6FFD007F291B159452265EFAE46B210D3B6682A"),
                PasswordHashKey = Encoding.ASCII.GetBytes("0x183DA481D881458F417E33778DF0A99A9FF8123BCB4A3EE9087CFC2AB2FE4BB2013B7EDE791CBEE161E36BBC88D38B8789C343A71FFCE942BF595E649D945ACA500B7AA72CA0D93F4E6B20BA13EEA5D26FF44322FD0EBA445DA7E33419B0838C274ACC4CC15291B56EC95A7493A1ECC6B8094DE6A2C335ED6A71078B16F7ECA0")
            };
            var repo = await repository.Add(customer);
            var result =await  repository.Delete(repo.Id);
            Assert.IsNotNull(result);
        }

        [Test]
        public  async Task UpdateTest()
        {
            IRepository<int, User> repository1 = new UserRepository(context);
            
            var customer2 = await repository1.Get(1);
            customer2.Phone = "1234561234";
            var res =await  repository1.Update(customer2);
            Assert.AreEqual(1, res.Id);
        }

        [Test]
        public async Task GetTest()
        {
            IRepository<int, User> repository = new UserRepository(context);
            User customer = new User()
            {
                Id = 4,
                Phone = "1234567899",
                Name = "vk",
                Role = "Customer",
                Password = Encoding.ASCII.GetBytes("0xF2AFD30723ADD33EA4185F0E8DC0581D3E12897EFFFE775FD1172CF21CD72CC7888BC8D7E6D6463CBEA3BBEDF6FFD007F291B159452265EFAE46B210D3B6682A"),
                PasswordHashKey = Encoding.ASCII.GetBytes("0x183DA481D881458F417E33778DF0A99A9FF8123BCB4A3EE9087CFC2AB2FE4BB2013B7EDE791CBEE161E36BBC88D38B8789C343A71FFCE942BF595E649D945ACA500B7AA72CA0D93F4E6B20BA13EEA5D26FF44322FD0EBA445DA7E33419B0838C274ACC4CC15291B56EC95A7493A1ECC6B8094DE6A2C335ED6A71078B16F7ECA0")
            };
            
            var rep = await repository.Add(customer);
            var result = await  repository.Get(rep.Id);
            Assert.AreEqual(result.Id,4);
        }

        [Test]
        public async Task GetAllTest()
        {
            IRepository<int, User> repository = new UserRepository(context);
            User customer = new User()
            {
                Id = 5,
                Phone = "1234567899",
                Name = "vk",
                Role = "Customer",
                Password = Encoding.ASCII.GetBytes("0xF2AFD30723ADD33EA4185F0E8DC0581D3E12897EFFFE775FD1172CF21CD72CC7888BC8D7E6D6463CBEA3BBEDF6FFD007F291B159452265EFAE46B210D3B6682A"),
                PasswordHashKey = Encoding.ASCII.GetBytes("0x183DA481D881458F417E33778DF0A99A9FF8123BCB4A3EE9087CFC2AB2FE4BB2013B7EDE791CBEE161E36BBC88D38B8789C343A71FFCE942BF595E649D945ACA500B7AA72CA0D93F4E6B20BA13EEA5D26FF44322FD0EBA445DA7E33419B0838C274ACC4CC15291B56EC95A7493A1ECC6B8094DE6A2C335ED6A71078B16F7ECA0")
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