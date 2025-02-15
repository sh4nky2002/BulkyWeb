using MyAspNetCoreApp.MyModels.Models;
using MyDataAccess.Data.Repository.IRepository;

namespace MyAspNetCoreApp.MyDataAccess.Repository.IRepository
{
    public interface ICompanyRepository: IRepository<CompanyModel>
    {
        void Update(CompanyModel obj);
    }
}