using Domain.CartNS;
using Domain.NotficationNS;
using Domain.OrderNS;
using Domain.Payment;
using Domain.ProductNS;
using Domain.UserNs;
using Domain.UserNS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace infrastructure
{
    public class HardwhereDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PaymentGateWay> PaymentGateWays { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<CategoreyImage> CategoreyImages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartContents { get; set; }
        public DbSet<Notfication> Notfications { get; set; }
        public DbSet<UserSearch> UserSearch { get; set; }
        public DbSet<Order> Orders { get; set; }

        public HardwhereDbContext(DbContextOptions<HardwhereDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            optionsBuilder.LogTo(
                Console.WriteLine,
                new[] { DbLoggerCategory.Database.Command.Name },
                LogLevel.Information
            );
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(
                    "User Id=postgres.hctgicuvarolobizspdr;Password=321HardWhere.ps@;Server=aws-0-eu-central-1.pooler.supabase.com;Port=5432;Database=postgres"
                );
            }
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

            modelBuilder
                .Entity<Address>()
                .HasOne(cp => cp.user)
                .WithMany()
                .HasForeignKey(cp => cp.userId)
                .IsRequired();

            modelBuilder
                .Entity<Order>()
                .HasOne(e => e.admin)
                .WithMany()
                .HasForeignKey(e => e.adminId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<Order>()
                .HasOne(e => e.BillingAdress)
                .WithMany()
                .HasForeignKey(e => e.BillingAddressId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<Order>()
                .HasOne(e => e.ShippingAddress)
                .WithMany()
                .HasForeignKey(e => e.ShippingAdressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Order>()
                .HasMany(c => c.contentes)
                .WithOne()
                .HasForeignKey(cp => cp.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder
                .Entity<OrderContents>()
                .HasOne(cp => cp.order)
                .WithMany(c => c.contentes)
                .HasForeignKey(cp => cp.OrderId)
                .IsRequired();
            modelBuilder
                .Entity<Notfication>()
                .HasOne(cp => cp.User)
                .WithMany()
                .HasForeignKey(cp => cp.userId)
                .IsRequired();
            ;
        }
    }
}
