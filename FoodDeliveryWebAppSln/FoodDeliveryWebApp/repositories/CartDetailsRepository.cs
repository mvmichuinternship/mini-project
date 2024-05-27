using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.exceptions;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.repositories.dummymodel;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

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
            return (await _context.CartDetails.SingleOrDefaultAsync(u => u.CartDetailsId == key));
        }

        public async Task<CartDetails> GetByCartId(int key)
        {
            return (await _context.CartDetails.SingleOrDefaultAsync(u => u.CartId == key)) ;
        }
        
        public async Task<IEnumerable<CartDetails>> GetallByCartId(int key)
        {
            return (await _context.CartDetails.Where(u => u.CartId == key).ToListAsync()) ;
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

        public async Task<List<CartDetails>> GetTotalForCart(int cartId)
        {
            var cartDetailsQuery = await _context.CartDetails
                .Where(cd => cd.CartId == cartId)
                .Select(cd => new CartDetails
                {
                    CartId = cd.CartId,
                    FId = cd.FId,
                    Qty_ordered = cd.Qty_ordered,
                    Total = cd.Total
                }).ToListAsync();

            //var cartItems = await cartDetailsQuery.Where(cd => cd.CartId == cartId).Select(cd => new CartItem
            //{
            //    Cid = cd.CartId,
            //    Fid = cd.FId,
            //    Quantity = cd.Qty_ordered,
            //    Total = cd.Total
            //}).ToListAsync();

            int total = await _context.CartDetails
                .Where(cd => cd.CartId == cartId)
                .SumAsync(cd => cd.Total);
            if (cartDetailsQuery.Count > 0 && total != 0)
            {

                return (cartDetailsQuery);
            }
            else
                throw new NoId("No cart with this Id");
        }
    }
}
