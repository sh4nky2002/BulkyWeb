using System.Linq.Expressions;
using MyAspNetCoreApp.Models;
using MyAspNetCoreApp.MyDataAccess.Repository;
using MyAspNetCoreApp.MyDataAccess.Repository.IRepository;
using MyAspNetCoreApp.MyModels.Models;
using MyDataAccess.Data;

public class ShoppingCartRepository : Repository<ShoppingCartModel>, IShoppingCartRepository
{

    private readonly CrudeContext _context;
    public ShoppingCartRepository(CrudeContext context) : base(context)
    {
        _context = context;
    }

    public void Update(ShoppingCartModel obj)
    {
        _context.ShoppingCarts.Update(obj);
}
}