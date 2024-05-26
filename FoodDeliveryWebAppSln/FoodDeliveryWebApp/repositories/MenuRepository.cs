using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.repositories
{
    public class MenuRepository : IRepository<int, Menu>
    {
        private FoodAppContext _context;

        public MenuRepository(FoodAppContext context)
        {
            _context = context;
        }
        public async Task<Menu> Add(Menu item)
        {

            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Menu> Delete(int key)
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

        public async Task<Menu> Get(int key)
        {
            return (await _context.Menus.SingleOrDefaultAsync(u => u.FId == key)) ?? throw new NoId("No user with the given ID");
        }

        public async Task<IEnumerable<Menu>> GetAll()
        {
            return await _context.Menus.ToListAsync();
        }

        public async Task<Menu> Update(Menu item)
        {
            var user = await Get(item.FId);
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
