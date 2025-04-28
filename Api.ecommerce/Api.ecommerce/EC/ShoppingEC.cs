using Api.ecommerce.Database;
using ced22b_cop4870_project1.Models;
using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Api.ecommerce.EC
{
    public class ShoppingEC
    {
        public List<Item?> Get()
        {
            return FakeDatabase.Cart;  
        }

        public IEnumerable<Item> Get(string? query)
        {
            return FakeDatabase.Cart.Where(item => item?.Product?.Name?.ToLower().Contains(query?.ToLower() ?? string.Empty) ?? false).Take(100);
        }

        public Item? Delete(int id)
        {
            var itemToDelete = FakeDatabase.Cart.FirstOrDefault(i => i?.Id == id);
            if (itemToDelete != null)
            {
                FakeDatabase.Cart.Remove(itemToDelete);
                var inventoryItem = FakeDatabase.Inventory.FirstOrDefault(i => i?.Id == id);
                if (inventoryItem != null)
                {
                    inventoryItem.Quantity++;
                }
            }
            return itemToDelete;
        }

        public Item? AddOrUpdate(Item item)
        {
            if (item.Id == 0)
            {
                item.Id = FakeDatabase.LastKey_Item + 1;
                FakeDatabase.Cart.Add(item);
            }
            else
            {
                var existingItem = FakeDatabase.Cart.FirstOrDefault(p => p.Id == item.Id);
                if (existingItem != null) 
                {
                    existingItem.Quantity = item.Quantity;
                }
                else
                {
                    FakeDatabase.Cart.Add(item);
                }
            }
            return item;
        }
    }
}
