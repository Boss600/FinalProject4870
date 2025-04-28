using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Api.ecommerce.Database
{
    public static class FakeDatabase
    {
        private static List<Item?> inventory = new List<Item?>
        {
            new Item{ Product = new ProductDTO{Id = 1, Name ="Product 1", Price = 5.99m}, Id = 1, Quantity = 1 },
            new Item { Product = new ProductDTO { Id = 2, Name = "Product 2", Price = 12.99m }, Id = 2, Quantity = 2 },
            new Item { Product = new ProductDTO { Id = 3, Name = "Product 3", Price = 2.99m }, Id = 3, Quantity = 3 }
        };

        private static List<Item?> cart = new List<Item?>();

        public static int LastKey_Item
        {
            get
            {
                if (!inventory.Any())
                {
                    return 0;
                }
                return inventory.Select(p => p?.Id ?? 0).Max();
            }
        }
        public static List<Item?> Inventory
        {
            get
            {
                return inventory;
            }

        }

        public static List<Item?> Cart
        {
            get
            {
                return cart;
            }
        }

        public static IEnumerable<Item> Search(string? query)
        {
            return Inventory.Where(p => p?.Product?.Name?.ToLower().Contains(query?.ToLower() ?? string.Empty) ?? false);
        }

        public static void ClearCart()
        {
            Cart.Clear();
        }
    }
}
