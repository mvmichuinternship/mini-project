using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.repositories
{
    public class CartRepository : IRepository<int, Cart>
    {
        private FoodAppContext _context;

        public CartRepository(FoodAppContext context)
        {
            _context = context;
        }
        public async Task<Cart> Add(Cart item)
        {

            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Cart> Delete(int key)
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

        public async Task<Cart> Get(int key)
        {
            return (await _context.Carts.SingleOrDefaultAsync(u => u.CartId == key)) ?? null ;
        }

        public async Task<IEnumerable<Cart>> GetAll()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task<Cart> Update(Cart item)
        {
            var user = await Get(item.CartId);
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
