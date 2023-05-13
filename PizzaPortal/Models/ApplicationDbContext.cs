using Microsoft.EntityFrameworkCore;
using System.Net;

namespace PizzaPortal.Models
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Pizza> Pizzas { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
