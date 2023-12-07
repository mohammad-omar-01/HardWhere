using Domain.Payment;
using Domain.Product;
using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace infrastructure
{
    public class HardwhereDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<PaymentGateWay> PaymentGateWays { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<CategoreyImage> CategoreyImages { get; set; }

        public HardwhereDbContext(DbContextOptions<HardwhereDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(
                Console.WriteLine,
                new[] { DbLoggerCategory.Database.Command.Name },
                LogLevel.Information
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ProductCategory>()
                .HasOne(p => p.CategoryImage)
                .WithOne(p => p.category)
                .HasForeignKey<CategoreyImage>(p => p.CategoreyId);
        }
    }
}
