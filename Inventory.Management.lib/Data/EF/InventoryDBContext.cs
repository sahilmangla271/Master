using Inventory.Management.Infrastructure.Data.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Management.Infrastructure.Data.EF
{
    public class InventoryDBContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Inventory.Management.Infrastructure.Data.EF.Model.Inventory> Inventories { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public InventoryDBContext(DbContextOptions<InventoryDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure relationships and constraints here
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=InventoryManagement.db");
        }

    }
}
