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

    }
}
