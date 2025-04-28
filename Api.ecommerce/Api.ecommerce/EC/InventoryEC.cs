using Api.ecommerce.Database;
using ced22b_cop4870_project1.Models;
using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Api.ecommerce.EC
{
    public class InventoryEC
    {
        public List<Item?> Get()
        {
            return FakeDatabase.Inventory;
        }

        public IEnumerable<Item> Get(string? query)
        {
            return FakeDatabase.Search(query).Take(100) ?? new List<Item>();
        }

        public Item? Delete(int id)
        {
            var itemToDelete = FakeDatabase.Inventory.FirstOrDefault(i => i?.Id == id);
            if (itemToDelete != null) 
            {
                FakeDatabase.Inventory.Remove(itemToDelete);
            }

            return itemToDelete;
        }

        public Item? AddOrUpdate(Item item)
        {
            if (item.Id == 0)
            {
                item.Id = FakeDatabase.LastKey_Item + 1;
                item.Product.Id = item.Id;
                FakeDatabase.Inventory.Add(item);
            }
            else 
            {
                var exisitingItem = FakeDatabase.Inventory.FirstOrDefault(p => p.Id == item.Id);
                var index = FakeDatabase.Inventory.IndexOf(exisitingItem);
                FakeDatabase.Inventory.RemoveAt(index);
                FakeDatabase.Inventory.Insert(index, new Item(item));
            }
            return item;
        }
    }
}
