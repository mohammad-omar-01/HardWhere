using Application.DTOs;
using Domain.CartNS;
using Domain.Payment;
using Domain.ProductNS;
using Domain.UserNS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace infrastructure
{
    public class HardwhereDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<PaymentGateWay> PaymentGateWays { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<CategoreyImage> CategoreyImages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartContents { get; set; }

        public HardwhereDbContext(DbContextOptions<HardwhereDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(
                Console.WriteLine,
                new[] { DbLoggerCategory.Database.Command.Name },
                LogLevel.Information
            );
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ProductCategory>()
                .HasOne(p => p.CategoryImage)
                .WithOne(p => p.category)
                .HasForeignKey<CategoreyImage>(p => p.CategoreyId);
            modelBuilder
                .Entity<User>()
                .HasMany(p => p.Products)
                .WithOne()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder
                .Entity<Cart>()
                .HasMany(c => c.contents)
                .WithOne()
                .HasForeignKey(cp => cp.CartId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder
                .Entity<CartProduct>()
                .HasOne(cp => cp.cart)
                .WithMany(c => c.contents)
                .HasForeignKey(cp => cp.CartId)
                .IsRequired();
        }
    }
}
