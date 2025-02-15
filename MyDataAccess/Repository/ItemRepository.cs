using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyAspNetCoreApp.Models;
using MyAspNetCoreApp.MyDataAccess.Repository;
using MyAspNetCoreApp.MyDataAccess.Repository.IRepository;
using MyDataAccess.Data;

public class ItemRepository : Repository<ItemModel>, IItemRepository
{

    private readonly CrudeContext _context;
    public ItemRepository(CrudeContext context) : base(context)
    {
        _context = context;
    }

    public void Update(ItemModel item)
    {
        _context.Items.Update(item);
    }
}