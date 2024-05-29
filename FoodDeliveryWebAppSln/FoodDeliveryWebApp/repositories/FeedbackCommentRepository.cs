using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.repositories
{
    public class FeedbackCommentRepository : IRepository<int, FbComment>
    {
        private FoodAppContext _context;

        public FeedbackCommentRepository(FoodAppContext context)
        {
            _context = context;
        }
        public async Task<FbComment> Add(FbComment item)
        {

            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<FbComment> Delete(int key)
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

        public async Task<FbComment> Get(int key)
        {
            return (await _context.FbComments.SingleOrDefaultAsync(u => u.CommentId == key)) ?? throw new NoId("No user with the given ID");
        }

        public async Task<FbComment> GetByFbId(int key)
        {
            return (await _context.FbComments.SingleOrDefaultAsync(u => u.FbId == key));
        }

        public async Task<IEnumerable<FbComment>> GetAll()
        {
            return await _context.FbComments.ToListAsync();
        }

        public async Task<FbComment> Update(FbComment item)
        {
            var user = await Get(item.CommentId);
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
