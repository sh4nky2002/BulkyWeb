using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyAspNetCoreApp.Models;
using MyAspNetCoreApp.MyModels.Models;

namespace MyDataAccess.Data
{
    public class CrudeContext : IdentityDbContext<IdentityUser>
    {
        public CrudeContext(DbContextOptions<CrudeContext> options) : base(options) { }

        public DbSet<ItemModel> Items { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<ApplicationUserModel> ApplicationUsers{get; set;}

        public DbSet<OrderDetailsModel> OrderDetails{get; set;}
        public DbSet<OrderHeaderModel> OrderHeaders{get; set;}


        public DbSet<ShoppingCartModel> ShoppingCarts{get; set;}

}
}
