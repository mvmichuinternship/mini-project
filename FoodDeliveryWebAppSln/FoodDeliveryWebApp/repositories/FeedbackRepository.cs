using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.repositories
{
    public class FeedbackRepository : IRepository<int, Feedback>
    {
        private FoodAppContext _context;

        public FeedbackRepository(FoodAppContext context)
        {
            _context = context;
        }
        public async Task<Feedback> Add(Feedback item)
        {

            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Feedback> Delete(int key)
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

        public async Task<Feedback> Get(int key)
        {
            return (await _context.Feedbacks.SingleOrDefaultAsync(u => u.FbId == key)) ?? throw new NoId("No user with the given ID");
        }

        public async Task<IEnumerable<Feedback>> GetAll()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        public async Task<Feedback> Update(Feedback item)
        {
            var user = await Get(item.FbId);
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
