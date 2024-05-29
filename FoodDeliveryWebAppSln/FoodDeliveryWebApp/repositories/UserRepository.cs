using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.repositories
{
    public class UserRepository : IRepository<int, User>
    {

        private FoodAppContext _context;

        
        public UserRepository(FoodAppContext context)
        {
            _context = context;
        }
        public async Task<User> Add(User item)
        {
           
            _context.Users.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<User> Delete(int key)
        {
            var user = await Get(key);
            if (user != null)
            {
                _context.Remove(user);
                await _context.SaveChangesAsync();
                return user;
            }
            throw new NoId("No user with the given ID");
        }

        public async Task<User> Get(int key)
        {
            return (await _context.Users.SingleOrDefaultAsync(u => u.Id == key)) ?? throw new NoId("No user with the given ID");
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> Update(User item)
        {
            var user = await Get(item.Id);
            if (user != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return user;
            }
            throw new NoId("No user with the given ID");
        }
    }
}
