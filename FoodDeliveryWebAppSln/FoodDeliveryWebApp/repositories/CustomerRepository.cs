using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.repositories
{
    public class CustomerRepository: IRepository<int,Customer>
    {
        private FoodAppContext _context;

        public CustomerRepository(FoodAppContext context)
        {
            _context = context;
        }
        public async Task<Customer> Add(Customer item)
        {

            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Customer> Delete(int key)
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

        public async Task<Customer> Get(int key)
        {
            return (await _context.Customers.SingleOrDefaultAsync(u => u.Id == key)) ?? throw new NoId("No user with the given ID");
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> Update(Customer item)
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
