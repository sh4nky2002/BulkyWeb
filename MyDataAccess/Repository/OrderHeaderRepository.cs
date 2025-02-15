using System.Linq.Expressions;
using MyAspNetCoreApp.Models;
using MyAspNetCoreApp.MyDataAccess.Repository;
using MyAspNetCoreApp.MyDataAccess.Repository.IRepository;
using MyAspNetCoreApp.MyModels.Models;
using MyDataAccess.Data;

public class OrderHeaderRepository : Repository<OrderHeaderModel>, IOrderHeaderRepository
{

    private readonly CrudeContext _context;
    public OrderHeaderRepository(CrudeContext context) : base(context)
    {
        _context = context;
    }

    public void Update(OrderHeaderModel obj)
    {
    _context.OrderHeaders.Update(obj);    
    }
}