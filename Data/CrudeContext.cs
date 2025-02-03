using Microsoft.EntityFrameworkCore;
using MyAspNetCoreApp.Models;


public class CrudeContext : DbContext
{

    public CrudeContext(DbContextOptions<CrudeContext> options) : base(options) { }
        public DbSet<ItemModel> Items { get; set; }
  // protected override void OnModelCreating(ModelBuilder modelBuilder)
   // {
        // Optional: You can add seeding data here if needed in the future.
   // }
}
