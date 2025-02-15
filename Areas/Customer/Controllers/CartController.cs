using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyAspNetCoreApp.MyDataAccess.Repository;
using MyAspNetCoreApp.MyDataAccess.Repository.IRepository;
using MyAspNetCoreApp.MyModels.Models;
using MyAspNetCoreApp.MyModels.ViewModels;
using MyAspNetCoreApp.MyUtility;
using NuGet.Common;

namespace MyAspNetCoreApp.Areas.Customer.Controllers{
    [Area("Customer")]
    [Authorize]
public class CartController : Controller
{
    private readonly IUnitOfWork _unitofwork;
    [BindProperty]
    public ShoppingCartVM ShoppingCartVM{get; set;}
    public CartController(IUnitOfWork unitofwork)
    {
        _unitofwork=unitofwork;
    }
    public IActionResult Index()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId=claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        ShoppingCartVM = new(){
            ShoppingCartList = _unitofwork.ShoppingCart.GetAll(u=>u.ApplicationUserId==userId,
            includeProperties:"Product"),
            OrderHeader = new()
        };

        foreach(var cart in ShoppingCartVM.ShoppingCartList){
            cart.Price=GetPriceBasedOnQuantity(cart);
            ShoppingCartVM.OrderHeader.OrderTotal+=(cart.Price*cart.count);
        }
    return View(ShoppingCartVM);
    }
    public IActionResult Summary(){
         var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId=claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        ShoppingCartVM = new(){
            ShoppingCartList = _unitofwork.ShoppingCart.GetAll(u=>u.ApplicationUserId==userId,
            includeProperties:"Product"),
            OrderHeader = new()
        };
        ShoppingCartVM.OrderHeader.ApplicationUser=_unitofwork.ApplicationUser.Get(u=>u.Id==userId);
        ShoppingCartVM .OrderHeader.Name=ShoppingCartVM.OrderHeader.ApplicationUser.Name;
        ShoppingCartVM .OrderHeader.PhoneNumber=ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
        ShoppingCartVM .OrderHeader.StreetAddress=ShoppingCartVM.OrderHeader.ApplicationUser.StringAddress;
        ShoppingCartVM .OrderHeader.City=ShoppingCartVM.OrderHeader.ApplicationUser.City;
        ShoppingCartVM .OrderHeader.State=ShoppingCartVM.OrderHeader.ApplicationUser.State;
        ShoppingCartVM .OrderHeader.PostalCode=ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;


        foreach(var cart in ShoppingCartVM.ShoppingCartList){
            cart.Price=GetPriceBasedOnQuantity(cart);
            ShoppingCartVM.OrderHeader.OrderTotal+=(cart.Price*cart.count);
        }
        return View(ShoppingCartVM);
        
    }

[HttpPost]
[ActionName("Summary")]
    public IActionResult SummaryPOST(){
         var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId=claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM.ShoppingCartList = _unitofwork.ShoppingCart.GetAll(u=>u.ApplicationUserId==userId,
            includeProperties:"Product");

            ShoppingCartVM.OrderHeader.OrderDate= System.DateTime.Now;
            ShoppingCartVM.OrderHeader.ApplicationUserId=userId;
 
        ApplicationUserModel applicationUser=_unitofwork.ApplicationUser.Get(u=>u.Id==userId);

        // ShoppingCartVM .OrderHeader.Name=ShoppingCartVM.OrderHeader.ApplicationUser.Name;
        // ShoppingCartVM .OrderHeader.PhoneNumber=ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
        // ShoppingCartVM .OrderHeader.StreetAddress=ShoppingCartVM.OrderHeader.ApplicationUser.StringAddress;
        // ShoppingCartVM .OrderHeader.City=ShoppingCartVM.OrderHeader.ApplicationUser.City;
        // ShoppingCartVM .OrderHeader.State=ShoppingCartVM.OrderHeader.ApplicationUser.State;
        // ShoppingCartVM .OrderHeader.PostalCode=ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;


        foreach(var cart in ShoppingCartVM.ShoppingCartList){
            cart.Price=GetPriceBasedOnQuantity(cart);
            ShoppingCartVM.OrderHeader.OrderTotal+=(cart.Price*cart.count);
        }

        if(applicationUser.CompanyId.GetValueOrDefault()==0){
            ShoppingCartVM.OrderHeader.PaymentStatus=StaticDetails.PaymentStatusPending;
            ShoppingCartVM.OrderHeader.OrderStatus=StaticDetails.StatusPending;
        }
        else
        {
            ShoppingCartVM.OrderHeader.PaymentStatus=StaticDetails.PaymentStatusDelayedPayment;
            ShoppingCartVM.OrderHeader.OrderStatus=StaticDetails.StatusApproved;
        }
        _unitofwork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
        _unitofwork.Save();

        foreach(var cart in ShoppingCartVM.ShoppingCartList){
            OrderDetailsModel orderDetails=new(){
            ProductId=cart.ProductId,
            OrderHeaderId=ShoppingCartVM.OrderHeader.Id,
            Price=cart.Price,
            Count=cart.count
            };
            _unitofwork.OrderDetail.Add(orderDetails);
            _unitofwork.Save();
        }
        if(applicationUser.CompanyId.GetValueOrDefault()==0){

        }
        return RedirectToAction(nameof(OrderConfirmation),new {id=ShoppingCartVM.OrderHeader.Id});
        
    }
    public IActionResult OrderConfirmation(int id)
    {
        return View(id);
    }
    public IActionResult Plus(int cartId)
    {
    var cartfromdb =_unitofwork.ShoppingCart.Get(u=>u.Id==cartId);
    cartfromdb.count +=1;
    _unitofwork.ShoppingCart.Update(cartfromdb);
    _unitofwork.Save();
    return RedirectToAction(nameof(Index));
    }
     public IActionResult Minus(int cartId)
    {
    var cartfromdb =_unitofwork.ShoppingCart.Get(u=>u.Id==cartId);
    if(cartfromdb.count<=1)
    {
    _unitofwork.ShoppingCart.Remove(cartfromdb);

    }
    else{
    cartfromdb.count -=1;
        _unitofwork.ShoppingCart.Update(cartfromdb);
    }
    _unitofwork.Save();
    return RedirectToAction(nameof(Index));
    }
      public IActionResult Remove(int cartId)
    {
    var cartfromdb =_unitofwork.ShoppingCart.Get(u=>u.Id==cartId);
    _unitofwork.ShoppingCart.Remove(cartfromdb);
    _unitofwork.Save();
    return RedirectToAction(nameof(Index));
    }
    private double GetPriceBasedOnQuantity(ShoppingCartModel shoppingCart)
    {
        if(shoppingCart.count<=50)
        {
            return shoppingCart.Product.Price;
        }
        else{
            if(shoppingCart.count<=100){
                return shoppingCart.Product.Price50;
            }
            else
            {
                return shoppingCart.Product.Price100;
            }
        }
    }
}
}