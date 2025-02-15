using MyAspNetCoreApp.Models;
using MyDataAccess.Data.Repository.IRepository;

namespace MyAspNetCoreApp.MyDataAccess.Repository.IRepository
{
    public interface IItemRepository: IRepository<ItemModel>
    {
        void Update(ItemModel item);
    }
}