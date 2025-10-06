using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BearerToken.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext():base("AppDbContext")
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedItem> OrderedItems { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}