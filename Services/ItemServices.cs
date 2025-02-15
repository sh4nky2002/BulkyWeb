using System.Collections.Generic;
using System.Linq;
using MyDataAccess.Data;
namespace MyAspNetCoreApp.Models;// Ensure this namespace is correct

public class ItemServices : IItemServices
{
    private readonly CrudeContext _context;

    public ItemServices(CrudeContext context)
    {
        _context = context;
    }

    public IEnumerable<ItemModel> GetItems() => _context.Items.ToList();

    public ItemModel GetItemById(int id) => _context.Items.Find(id);

    public void AddItem(ItemModel item)
    {
        _context.Items.Add(item);
        _context.SaveChanges();
    }

    public void UpdateItem(ItemModel item)
    {
        _context.Items.Update(item);
        _context.SaveChanges();
    }

    public void DeleteItem(int id)
    {
        var item = _context.Items.Find(id);
        if (item != null)
        {
            _context.Items.Remove(item);
            _context.SaveChanges();
        }
    }
}