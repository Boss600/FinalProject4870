using ced22b_cop4870_project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.eCommerce.Services
{
   public class ShoppingCartServiceProxy
    {
        private ShoppingCartServiceProxy() 
        {
            CartProducts = new List<CartProduct?>();
        }

        private int LastKey
        {
            get
            {
                if (!CartProducts.Any())
                {
                    return 0;
                }

                return CartProducts.Select(c => c?.Id ?? 0).Max();
            }
        }

        private static ShoppingCartServiceProxy? instance;
        private static object instanceLock = new object();
        public static ShoppingCartServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartServiceProxy();
                    }
                }
                return instance;
            }
        }
        public List<CartProduct?> CartProducts { get; private set; }

        public CartProduct AddOrUpdate(CartProduct cartProduct)
        {
            if(cartProduct.Id == 0)
            {
                cartProduct.Id = LastKey + 1;
                CartProducts.Add(cartProduct);
            }

            return cartProduct;
        }

        public CartProduct? Delete(int id) 
        {
            if (id == 0)
            {
                return null;
            }

            CartProduct? cartProduct = CartProducts.FirstOrDefault(c => c.Id == id);
            CartProducts.Remove(cartProduct);

            return cartProduct;
        }


    }
}
