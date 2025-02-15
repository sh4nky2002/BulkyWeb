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
using MyAspNetCoreApp.MyModels.Models;

namespace MyAspNetCoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Roles =StaticDetails.Role_Admin)]

public class CompanyController : Controller
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
    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: Item
    public IActionResult Index()
    {
        var companylist = _unitOfWork.Company.GetAll().ToList();
        return View(companylist);
    }
   // GET: Item/Create
   [HttpGet]
public IActionResult Upsert(int? id)
{
    //  IEnumerable<SelectListItem> ItemList = 
        // ViewBag.ItemList = ItemList;
        // ViewData["ItemList"] = ItemList;
     
        if(id == null || id == 0)
        {
            // create
            return View(new CompanyModel());
        }
        else
        {
            // update
            CompanyModel companyModel = _unitOfWork.Company.Get(i=>i.Id==id);
            return View(companyModel);
        }
        
 
}

// POST: Item/Create
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Upsert(CompanyModel CompanyObj)
{
    if (ModelState.IsValid)
    {
        if(CompanyObj.Id == 0)
        {
            _unitOfWork.Company.Add(CompanyObj);
        }
        else
        {
            _unitOfWork.Company.Update(CompanyObj);
        }
        _unitOfWork.Save() ;// Save the item
        TempData["Success"] = "Company created successfully"; // Set a success message
        return RedirectToAction(nameof(Index)); // Redirect to the index page
    }
    else
    {
    return View(CompanyObj); 
    }
// If validation fails, return to the create view with the item
}
    // GET: Item/Delete/5
    public IActionResult Delete(int id)
    {
        var company = _unitOfWork.Company.Get(company=>company.Id==id);
        if (company == null)
        {
            return NotFound();
        }
        return View(company);
    }

    // POST: Item/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {

        CompanyModel obj =_unitOfWork.Company.Get(company=>company.Id==id);
        if (obj == null)
        {
            return NotFound();
        }
        _unitOfWork.Company.Remove(obj);
        _unitOfWork.Save();
        TempData["Success"] = "Item deleted successfully";
        return RedirectToAction(nameof(Index));
    }
    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {      
          var objcompanylist = _unitOfWork.Company.GetAll().ToList();

        return Json(new { data = objcompanylist });
    }
    [HttpDelete]
     public IActionResult Delete(int? id)
    {      
        var companytobedeleted = _unitOfWork.Company.Get(i=>i.Id==id);
        if(companytobedeleted == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }
               _unitOfWork.Company.Remove(companytobedeleted);
               _unitOfWork.Save();
          return  Json (new { success = true, message = "Delete successful" });
    }
    #endregion
}
}
