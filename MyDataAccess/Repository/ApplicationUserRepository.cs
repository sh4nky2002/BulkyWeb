using System.Linq.Expressions;
using MyAspNetCoreApp.Models;
using MyAspNetCoreApp.MyDataAccess.Repository;
using MyAspNetCoreApp.MyDataAccess.Repository.IRepository;
using MyAspNetCoreApp.MyModels.Models;
using MyDataAccess.Data;

namespace MyAspNetCoreApp.MyDataAccess.Repository.IRepository{
public class ApplicationUserRepository : Repository<ApplicationUserModel>, IApplicationUserRepository
{

    private readonly CrudeContext _context;
    public ApplicationUserRepository(CrudeContext context) : base(context)
    {
        _context = context;
    }

}
}