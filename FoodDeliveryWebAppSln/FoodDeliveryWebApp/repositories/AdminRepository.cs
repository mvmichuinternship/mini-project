using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.repositories
{
    public class AdminRepository: IRepository<int, Admin>
    {

        private FoodAppContext _context;

        public AdminRepository(FoodAppContext context)
        {
            _context = context;
        }
        public async Task<Admin> Add(Admin item)
        {

            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Admin> Delete(int key)
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

        public async Task<Admin> Get(int key)
        {
            return (await _context.Admins.SingleOrDefaultAsync(u => u.Id == key)) ?? throw new NoId("No user with the given ID");
        }

        public async Task<IEnumerable<Admin>> GetAll()
        {
            return await _context.Admins.ToListAsync();
        }

        public async Task<Admin> Update(Admin item)
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
