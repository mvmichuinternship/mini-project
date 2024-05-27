using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.repositories
{
    public class PaymentRepository : IRepository<int, Payment>
    {
        private FoodAppContext _context;

        public PaymentRepository(FoodAppContext context)
        {
            _context = context;
        }
        public async Task<Payment> Add(Payment item)
        {

            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Payment> Delete(int key)
        {
            var payment = await Get(key);
            if (payment != null)
            {
                _context.Remove(payment);
                await _context.SaveChangesAsync();
                return payment;
            }
            throw new NoId("No payment with the given ID");
        }

        public async Task<Payment> Get(int key)
        {
            return (await _context.Payments.SingleOrDefaultAsync(u => u.PayId == key)) ?? throw new NoId("No payment with the given ID");
        }

        public async Task<IEnumerable<Payment>> GetAll()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment> Update(Payment item)
        {
            var payment = await Get(item.PayId);
            if (payment != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return payment;
            }
            throw new NoId("No payment with the given ID");
        }
    }
}
