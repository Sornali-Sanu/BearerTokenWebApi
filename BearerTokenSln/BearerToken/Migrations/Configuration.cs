namespace BearerToken.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BearerToken.Models.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BearerToken.Models.AppDbContext context)
        {
            context.Products.AddOrUpdate(p => p.ProductId,
               new Models.Product() { ProductId = 1, ProductName = "computer" }, 
               new Models.Product() { ProductId = 2, ProductName = "Mobile" },
               new Models.Product() { ProductId = 3, ProductName = "Laptop" });
            context.Users.AddOrUpdate(u => u.UserId,
                new Models.User() { UserId = 1, UserName = "Sornali", Email = "sornali@gmail.com", Password = "12345", Role = "Admin,User" },
                new Models.User() { UserId = 2, UserName = "Maliha", Email = "maliha@gmail.com", Password = "12345", Role = "User" }
                );
        }
    }
}
