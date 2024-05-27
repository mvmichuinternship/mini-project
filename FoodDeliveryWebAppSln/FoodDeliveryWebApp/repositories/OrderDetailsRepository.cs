using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.repositories
{
    public class OrderDetailsRepository : IRepository<int, OrderDetails>
    {
        private FoodAppContext _context;

        public OrderDetailsRepository(FoodAppContext context)
        {
            _context = context;
        }
        public async Task<OrderDetails> Add(OrderDetails item)
        {

            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<OrderDetails> Delete(int key)
        {
            var orderDetail = await Get(key);
            if (orderDetail != null)
            {
                _context.Remove(orderDetail);
                await _context.SaveChangesAsync();
                return orderDetail;
            }
            throw new NoId("No Order Detail with the given ID");
        }

        public async Task<OrderDetails> Get(int key)
        {
            return (await _context.OrderDetails.SingleOrDefaultAsync(u => u.OrderDetailsId == key));
        }

        public async Task<OrderDetails> GetByOrderId(int key)
        {
            return (await _context.OrderDetails.SingleOrDefaultAsync(u => u.OId == key));
        }

        public async Task<IEnumerable<OrderDetails>> GetallByOrderId(int key)
        {
            return (await _context.OrderDetails.Where(u => u.OId == key).ToListAsync());
        }

        public async Task<IEnumerable<OrderDetails>> GetAll()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetails> Update(OrderDetails item)
        {
            var orderDetail = await Get(item.OrderDetailsId);
            if (orderDetail != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return orderDetail;
            }
            throw new NoId("No Order Detail with the given ID");
        }

        public async Task<List<OrderDetails>> GetTotalForOrder(int orderId)
        {
            var orderDetailsQuery = await _context.OrderDetails
                .Where(cd => cd.OId == orderId)
                .Select(cd => new OrderDetails
                {
                    OId = cd.OId,
                    FId = cd.FId,
                    Qty_ordered = cd.Qty_ordered,
                    Total = cd.Total
                }).ToListAsync();

            int total = await _context.OrderDetails
                .Where(cd => cd.OId == orderId)
                .SumAsync(cd => cd.Total);
            if (orderDetailsQuery.Count > 0 && total != 0)
            {

                return (orderDetailsQuery);
            }
            else
                throw new NoId("No order with this Id");
        }
    }
}
