using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.repositories
{
    public class OrderRepository : IRepository<int, Order>
    {
        private FoodAppContext _context;

        public OrderRepository(FoodAppContext context)
        {
            _context = context;
        }
        public async Task<Order> Add(Order item)
        {

            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Order> Delete(int key)
        {
            var order = await Get(key);
            if (order != null)
            {
                _context.Remove(order);
                await _context.SaveChangesAsync();
                return order;
            }
            throw new NoId("No order with the given ID");
        }

        public async Task<Order> Get(int key)
        {
            return (await _context.Orders.SingleOrDefaultAsync(u => u.OId == key));
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> Update(Order item)
        {
            var order = await Get(item.OId);
            if (order != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return order;
            }
            throw new NoId("No order with the given ID");
        }
    }
}
