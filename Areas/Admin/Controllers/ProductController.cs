using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAspNetCoreApp.Models;
using MyAspNetCoreApp.MyDataAccess.Repository.IRepository;
using MyAspNetCoreApp.MyModels.ViewModels;
using MyDataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using MyAspNetCoreApp.MyUtility;

namespace MyAspNetCoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Roles =StaticDetails.Role_Admin)]

public class ProductController : Controller
{
    // private readonly IItemServices _itemServices;

    // public ItemController(IItemServices itemServices)
    // {
    //     _itemServices = itemServices;
    // }

    // private readonly IItemRepository _Itemrepo;
    // public ItemController(IItemRepository db)
    // {
    //     _Itemrepo = db;
        
    // }
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webhostingEnvironment;
    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webhostingEnvironment = webHostEnvironment;
    }

    // GET: Item
    public IActionResult Index()
    {
        var productlist = _unitOfWork.Product.GetAll(includeProperties:"Item").ToList();
        return View(productlist);
    }
   // GET: Item/Create
   [HttpGet]
public IActionResult Upsert(int? id)
{
    //  IEnumerable<SelectListItem> ItemList = 
        // ViewBag.ItemList = ItemList;
        // ViewData["ItemList"] = ItemList;
        ProductVM productVM = new()
        {
         ItemList= _unitOfWork.Item.GetAll().Select(i => new SelectListItem
        {
            Text = i.Name,
            Value = i.Id.ToString()
        }),
            ProductModel = new ProductModel(),
        };
        if(id == null || id == 0)
        {
            // create
            return View(productVM);
        }
        else
        {
            // update
            productVM.ProductModel = _unitOfWork.Product.Get(i=>i.Id==id);
            return View(productVM);
        }
        
 
}

// POST: Item/Create
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Upsert(ProductVM productVM,IFormFile? file)
{
    if (ModelState.IsValid)
    {
        string wwwRootPath = _webhostingEnvironment.WebRootPath;
        if(file!=null)
        {
            string fileName = Guid.NewGuid().ToString()+ Path.GetExtension(file.FileName);
            string productPath= Path.Combine(wwwRootPath, @"images/products");
            if(!string.IsNullOrEmpty(productVM.ProductModel.ImageUrl))
            {
                // delete the old image
               var oldImagePath= 
               Path.Combine(wwwRootPath,productVM.ProductModel.ImageUrl.TrimStart('\\'));
               if (System.IO.File.Exists(oldImagePath))
               {
                   System.IO.File.Delete(oldImagePath);
               }
            }
            using(var fileStream = new FileStream(Path.Combine(productPath,fileName),FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
           productVM.ProductModel.ImageUrl = @"\images\products\"+fileName;
        }
        if(productVM.ProductModel.Id == 0)
        {
            _unitOfWork.Product.Add(productVM.ProductModel);
        }
        else
        {
            _unitOfWork.Product.Update(productVM.ProductModel);
        }
        _unitOfWork.Save() ;// Save the item
        TempData["Success"] = "Product created successfully"; // Set a success message
        return RedirectToAction(nameof(Index)); // Redirect to the index page
    }
    else
    {
         productVM.ItemList= _unitOfWork.Item.GetAll().Select(i => new SelectListItem
        {
            Text = i.Name,
            Value = i.Id.ToString()
        });
    return View(productVM); 
    }
// If validation fails, return to the create view with the item
}
    // GET: Item/Delete/5
    // public IActionResult Delete(int id)
    // {
    //     var product = _unitOfWork.Product.Get(product=>product.Id==id);
    //     if (product == null)
    //     {
    //         return NotFound();
    //     }
    //     return View(product);
    // }

    // POST: Item/Delete/5
    // [HttpPost, ActionName("Delete")]
    // [ValidateAntiForgeryToken]
    // public IActionResult DeleteConfirmed(int id)
    // {

    //     ProductModel obj =_unitOfWork.Product.Get(product=>product.Id==id);
    //     if (obj == null)
    //     {
    //         return NotFound();
    //     }
    //     _unitOfWork.Product.Remove(obj);
    //     _unitOfWork.Save();
    //     TempData["Success"] = "Item deleted successfully";
    //     return RedirectToAction(nameof(Index));
    // }
    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {      
          var productlist = _unitOfWork.Product.GetAll(includeProperties:"Item").ToList();

        return Json(new { data = productlist });
    }
    [HttpDelete]
     public IActionResult Delete(int? id)
    {      
        var producttobedeleted = _unitOfWork.Product.Get(i=>i.Id==id);
        if(producttobedeleted == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }
        var oldImagePath = Path.Combine(_webhostingEnvironment.WebRootPath,producttobedeleted.ImageUrl.TrimStart('\\'));
               if (System.IO.File.Exists(oldImagePath))
               {
                   System.IO.File.Delete(oldImagePath);
               }
               _unitOfWork.Product.Remove(producttobedeleted);
               _unitOfWork.Save();
          return  Json (new { success = true, message = "Delete successful" });
    }
    #endregion
}
}
