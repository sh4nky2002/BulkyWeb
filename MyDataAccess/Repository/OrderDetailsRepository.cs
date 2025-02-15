using System.Linq.Expressions;
using MyAspNetCoreApp.Models;
using MyAspNetCoreApp.MyDataAccess.Repository;
using MyAspNetCoreApp.MyDataAccess.Repository.IRepository;
using MyAspNetCoreApp.MyModels.Models;
using MyDataAccess.Data;

public class OrderDetailsRepository : Repository<OrderDetailsModel>, IOrderDetailsRepository
{

    private readonly CrudeContext _context;
    public OrderDetailsRepository(CrudeContext context) : base(context)
    {
        _context = context;
    }

    public void Update(OrderDetailsModel obj)
    {
_context.OrderDetails.Update(obj);
}
}