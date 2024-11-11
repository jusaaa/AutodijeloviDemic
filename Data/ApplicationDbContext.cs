using AutodijeloviDemic.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutodijeloviDemic.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definisanje preciznosti za decimalna svojstva
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(8, 2);  // 18 cifara ukupno, 4 decimale

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(8, 2);

            modelBuilder.Entity<ShoppingCart>()
                .Property(sc => sc.TotalAmount)
                .HasPrecision(8, 2);

            modelBuilder.Entity<ShoppingCartItem>()
                .Property(sci => sci.UnitPrice)
                .HasPrecision(8, 2);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<EmployeeOrder> EmployeeOrders { get; set; }
        public DbSet<EmployeeProduct> EmployeeProducts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
