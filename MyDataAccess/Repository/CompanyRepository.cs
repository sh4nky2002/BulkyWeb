using System.Linq.Expressions;
using MyAspNetCoreApp.Models;
using MyAspNetCoreApp.MyDataAccess.Repository;
using MyAspNetCoreApp.MyDataAccess.Repository.IRepository;
using MyAspNetCoreApp.MyModels.Models;
using MyDataAccess.Data;

public class CompanyRepository : Repository<CompanyModel>, ICompanyRepository
{

    private readonly CrudeContext _context;
    public CompanyRepository(CrudeContext context) : base(context)
    {
        _context = context;
    }

    public void Update(CompanyModel company)
    {
    var companyfromdb= _context.Companies.FirstOrDefault(s => s.Id == company.Id);

    if(companyfromdb != null)
    {
        
        companyfromdb.Name = company.Name;
        companyfromdb.PhoneNumber = company.PhoneNumber;
        companyfromdb.StreetAddress = company.StreetAddress;
        companyfromdb.City = company.City;
        companyfromdb.State = company.State;
        companyfromdb.PostalCode = company.PostalCode;
    }  
   
    
}
}