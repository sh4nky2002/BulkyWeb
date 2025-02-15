using MyAspNetCoreApp.MyDataAccess.Repository.IRepository;
namespace MyAspNetCoreApp.MyDataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IItemRepository Item{ get; }
        IProductRepository Product{ get; }

        ICompanyRepository Company {get;}

        IShoppingCartRepository ShoppingCart {get;}

        IApplicationUserRepository ApplicationUser {get;}
        
        IOrderDetailsRepository OrderDetail{get;}
        IOrderHeaderRepository OrderHeader{get;}

        void Save();
    }
}
