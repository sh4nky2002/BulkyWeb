using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Models;
using MyAspNetCoreApp.MyDataAccess.Repository.IRepository;
using MyAspNetCoreApp.MyModels.Models;

namespace MyAspNetCoreApp.Areas.Customer.Controllers{
[Area("Customer")]

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<ProductModel> productList = _unitOfWork.Product.GetAll(includeProperties:"Item");
        return View(productList);
    }
    public IActionResult Details(int productId)
    {
        ShoppingCartModel cartModel =new(){
                Product = _unitOfWork.Product.Get(u=>u.Id==productId,includeProperties:"Item"),
                count=1,
                ProductId=productId
        };
        return View(cartModel);
    }
    [HttpPost]
    [Authorize]
    public IActionResult Details(ShoppingCartModel shoppingcart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId= claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        shoppingcart.ApplicationUserId=userId;

        ShoppingCartModel cartfromdb = _unitOfWork.ShoppingCart.Get(u=>u.ApplicationUserId==userId && u.ProductId==shoppingcart.ProductId);
        if(cartfromdb!=null)
        {
            cartfromdb.count +=shoppingcart.count;
            _unitOfWork.ShoppingCart.Update(cartfromdb);
        }
        else{
            _unitOfWork.ShoppingCart.Add(shoppingcart);
        }
        TempData["success"]="Cart updated succesfully";
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
{
    return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
}
}