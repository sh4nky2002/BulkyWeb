using MyAspNetCoreApp.MyDataAccess.Repository.IRepository;
using MyAspNetCoreApp.MyModels.Models;
using MyDataAccess.Data;

namespace MyAspNetCoreApp.MyDataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
{
    private readonly CrudeContext _context;
    public IItemRepository Item { get; private set; }
    public IProductRepository Product { get; private set; }

    public ICompanyRepository Company {get; private set;}

    public IShoppingCartRepository ShoppingCart {get; private set;}
    public IApplicationUserRepository ApplicationUser{get; private set;}
    public IOrderDetailsRepository OrderDetail{get; private set;}
    public IOrderHeaderRepository OrderHeader{get; private set;}

    public UnitOfWork(CrudeContext context)
    {
        _context = context;
        ApplicationUser= new ApplicationUserRepository(_context);
        ShoppingCart =new ShoppingCartRepository(_context);
        Item = new ItemRepository(_context); // ✅ Use the actual implementation
        Product = new ProductRepository(_context); 
        Company=new CompanyRepository(_context);
        OrderDetail=new OrderDetailsRepository(_context);
        OrderHeader=new OrderHeaderRepository(_context);

    }

  public void Save()
    {
        _context.SaveChanges();
    }

    }
}