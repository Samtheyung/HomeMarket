using HomeMarket.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace HomeMarket.DbProfile
{
    public class HomeMarketDbContext : DbContext
    {
        public HomeMarketDbContext(DbContextOptions<HomeMarketDbContext> options) : base(options)
        {
        }
        // Define your DbSets (tables) here
        // public DbSet<YourEntity> YourEntities { get; set; }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //// Configure relationships and constraints here if needed
            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.Customer)
            //    .WithMany(c => c.Orders)
            //    .HasForeignKey(o => o.CustomerId);
            //modelBuilder.Entity<OrderItem>()
            //    .HasOne(oi => oi.Order)
            //    .WithMany(o => o.Items)
            //    .HasForeignKey(oi => oi.OrderId);
            //modelBuilder.Entity<OrderItem>()
            //    .HasOne(oi => oi.Product)
            //    .WithMany()
            //    .HasForeignKey(oi => oi.ProductId);


        }
    }
}
