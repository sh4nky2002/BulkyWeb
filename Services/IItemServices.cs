using System.Collections.Generic;
using MyAspNetCoreApp.Models;

public interface IItemServices
{
    IEnumerable<ItemModel> GetItems();
    ItemModel GetItemById(int id);
    void AddItem(ItemModel item);
    void UpdateItem(ItemModel item);
    void DeleteItem(int id);
}
