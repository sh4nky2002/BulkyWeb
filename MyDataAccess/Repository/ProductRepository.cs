using System.Linq.Expressions;
using MyAspNetCoreApp.Models;
using MyAspNetCoreApp.MyDataAccess.Repository;
using MyAspNetCoreApp.MyDataAccess.Repository.IRepository;
using MyDataAccess.Data;

public class ProductRepository : Repository<ProductModel>, IProductRepository
{

    private readonly CrudeContext _context;
    public ProductRepository(CrudeContext context) : base(context)
    {
        _context = context;
    }

    public void Update(ProductModel product)
    {
    var productfromdb= _context.Products.FirstOrDefault(s => s.Id == product.Id);
    if(productfromdb != null)
    {
        if(product.ImageUrl != null)
       {
        productfromdb.Title = product.Title;
        productfromdb.Description = product.Description;
        productfromdb.ISBN = product.ISBN;
        productfromdb.Author = product.Author;
        productfromdb.ListPrice = product.ListPrice;
        productfromdb.Price = product.Price;
        productfromdb.Price50 = product.Price50;
        productfromdb.Price100 = product.Price100;
        productfromdb.ItemId = product.ItemId;
       }
        if(product.ImageUrl != null)
        {
            productfromdb.ImageUrl = product.ImageUrl;
        }
    }
}
}