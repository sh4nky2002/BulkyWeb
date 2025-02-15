using MyAspNetCoreApp.Models;
using MyAspNetCoreApp.MyModels.Models;
using MyDataAccess.Data.Repository.IRepository;

namespace MyAspNetCoreApp.MyDataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository: IRepository<OrderHeaderModel>
    {
        void Update(OrderHeaderModel obj);
    }
}