using Microsoft.AspNetCore.Mvc;
namespace MyAspNetCoreApp.Models;
public class ItemController : Controller
{
    private readonly IItemServices _itemServices;

    public ItemController(IItemServices itemServices)
    {
        _itemServices = itemServices;
    }

    // GET: Item
    public IActionResult Index()
    {
        var items = _itemServices.GetItems();
        return View(items);
    }

    // GET: Item/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Item/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ItemModel item)
    {
        if (ModelState.IsValid)
        {
            _itemServices.AddItem(item);
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }

    // GET: Item/Edit/5
    public IActionResult Edit(int id)
    {
        var item = _itemServices.GetItemById(id);
        if (item == null)
        {
            return NotFound();
        }
        return View(item);
    }

    // POST: Item/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id, Name, Price, Discount, ImageUrl, IsPublished, DisplayInHomepage")] ItemModel item)
    {
        if (id != item.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _itemServices.UpdateItem(item);
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }

    // GET: Item/Delete/5
    public IActionResult Delete(int id)
    {
        var item = _itemServices.GetItemById(id);
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
        _itemServices.DeleteItem(id);
        return RedirectToAction(nameof(Index));
    }
}
