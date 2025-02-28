using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ced22b_cop4870_project1.Models;
using Library.eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class ShoppingCartService
    {
        private ProductServiceProxy _prodSvc;
        private List<Item> items;
        public List<Item> CartItems { 
            get
            {
                return items;
            } 
        }
        public static ShoppingCartService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShoppingCartService();
                }
                return instance;
            }
        }

        private static ShoppingCartService? instance;

        private ShoppingCartService() {
            items = new List<Item>();
        }
    }
}
