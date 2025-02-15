using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Models;
using MyAspNetCoreApp.MyDataAccess.Repository.IRepository;
using MyAspNetCoreApp.MyUtility;

namespace MyAspNetCoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Roles =StaticDetails.Role_Admin)]

public class ItemController : Controller
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
    public ItemController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: Item
    public IActionResult Index()
    {
        var items = _unitOfWork.Item.GetAll().ToList();
        return View(items);
    }
   // GET: Item/Create
   [HttpGet]
public IActionResult Create()
{
    return View(); // This just returns the view with no parameters
}

// POST: Item/Create
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(ItemModel item)
{
    if (ModelState.IsValid)
    {

        _unitOfWork.Item.Add(item);
        _unitOfWork.Save() ;// Save the item
        return RedirectToAction(nameof(Index)); // Redirect to the index page
    }

    return View(item); // If validation fails, return to the create view with the item
}

    // GET: Item/Edit/5
        public IActionResult Edit(int id)
        {
            var item = _unitOfWork.Item.Get(item => item.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Item/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, Name, Price, Discount, IsPublished, DisplayInHomepage")] ItemModel item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            // Get the existing item from the database
            var existingItem = _unitOfWork.Item.Get(i => i.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                _unitOfWork.Item.Update(item);
                _unitOfWork.Save(); // Save the updated item
                return RedirectToAction(nameof(Index)); // Redirect to the index page
            }

            return View(item); // If validation fails, return to the edit view with the item
        }


    // GET: Item/Delete/5
    public IActionResult Delete(int id)
    {
        var item = _unitOfWork.Item.Get(item=>item.Id==id);
        if (item == null)
        {
            return NotFound();
        }
        return View(item);
    }

    // POST: Item/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {

        ItemModel obj =_unitOfWork.Item.Get(item=>item.Id==id);
        if (obj == null)
        {
            return NotFound();
        }
        _unitOfWork.Item.Remove(obj);
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }
}
}
