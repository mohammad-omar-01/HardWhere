using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace infrastructure
{
    public class HardwhereDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

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
    }
}
