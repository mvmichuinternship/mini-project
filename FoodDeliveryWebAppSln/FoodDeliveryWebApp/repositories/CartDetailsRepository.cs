using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.repositories
{
    public class CartDetailsRepository:IRepository<int,CartDetails>
    {
        private FoodAppContext _context;

        public CartDetailsRepository(FoodAppContext context)
        {
            _context = context;
        }
        public async Task<CartDetails> Add(CartDetails item)
        {

            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<CartDetails> Delete(int key)
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

        public async Task<CartDetails> Get(int key)
        {
            return (await _context.CartDetails.SingleOrDefaultAsync(u => u.CartDetailsId == key)) ?? throw new NoId("No user with the given ID");
        }

        public async Task<IEnumerable<CartDetails>> GetAll()
        {
            return await _context.CartDetails.ToListAsync();
        }

        public async Task<CartDetails> Update(CartDetails item)
        {
            var user = await Get(item.CartDetailsId);
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
