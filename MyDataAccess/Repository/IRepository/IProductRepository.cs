using MyAspNetCoreApp.Models;
using MyDataAccess.Data.Repository.IRepository;

namespace MyAspNetCoreApp.MyDataAccess.Repository.IRepository
{
    public interface IProductRepository: IRepository<ProductModel>
    {
        void Update(ProductModel product);
    }
}