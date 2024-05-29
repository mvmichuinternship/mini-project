using FoodDeliveryWebApp.models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.context
{
    public class FoodAppContext: DbContext
    {
        public FoodAppContext(DbContextOptions options) : base(options) 
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Menu> Menus { get; set; }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        
        public DbSet<FbComment> FbComments { get; set; }
    }
}
