using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.Models;
namespace InventoryManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ItemTransaction> ItemTransactions { get; set; }
    }
}
