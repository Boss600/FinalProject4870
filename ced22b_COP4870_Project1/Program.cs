using ced22b_cop4870_project1.Models;
using Library.eCommerce.Services;
using System;
using System.Xml.Serialization;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Amazon!");

            Console.WriteLine("1. Manage inventory");
            Console.WriteLine("2. Manage shopping cart");
            Console.WriteLine("3. Quit");

            char choice;
            do
            {
                string? input = Console.ReadLine();
                choice = input[0];
                switch (choice)
                {
                    case '1':
                        ManageInventory();
                        break;
                    case '2':
                        ManageShoppingCart();
                        break;
                    case '3':
                        break;
                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }
            } while (choice != '3');
        }
        static void ManageInventory()
        {
            Console.WriteLine("C. Create new inventory item");
            Console.WriteLine("R. Read new inventory item");
            Console.WriteLine("U. Update an inventory item");
            Console.WriteLine("D. Delete an inventory item");
            Console.WriteLine("Q. Quit");

            List<Product?> list = ProductServiceProxy.Current.Products;

            char inventoryChoice;
            do
            {
                string? input = Console.ReadLine();
                inventoryChoice = input[0];
                switch (inventoryChoice)
                {
                    case 'C':
                    case 'c':
                        ProductServiceProxy.Current.AddOrUpdate(new Product
                        {
                            Name = Console.ReadLine()
                        });
                        break;
                    case 'R':
                    case 'r':
                        list.ForEach(Console.WriteLine);
                        break;
                    case 'U':
                    case 'u':
                        Console.WriteLine("Which product would you like to update?");
                        int selection = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedProd = list.FirstOrDefault(p => p.Id == selection);

                        if (selectedProd != null)
                        {
                            selectedProd.Name = Console.ReadLine() ?? "ERROR";
                            ProductServiceProxy.Current.AddOrUpdate(selectedProd);
                        }
                        break;
                    case 'D':
                    case 'd':
                        Console.WriteLine("Which product would you like to update?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        ProductServiceProxy.Current.Delete(selection);
                        break;
                    case 'Q':
                    case 'q':
                        break;
                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }
            } while (inventoryChoice != 'Q' && inventoryChoice != 'q');

            Console.ReadLine();
        }

        static void ManageShoppingCart()
        {
            Console.WriteLine("A. Add item into shopping cart");
            Console.WriteLine("R. Read new shopping cart item");
            Console.WriteLine("U. Update new shopping cart item");
            Console.WriteLine("D. Delete a shopping cart item");
            Console.WriteLine("Q. Quit");

            List<CartProduct?> list = ShoppingCartServiceProxy.Current.CartProducts;

            char cartChoice;
            do
            {
                string? input = Console.ReadLine();
                cartChoice = input[0];
                switch (cartChoice)
                {
                    case 'A':
                    case 'a':
                        Console.WriteLine("Which product would you like to add to your cart(index)?");
                        int selection = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedID = ProductServiceProxy.Current.Products.FirstOrDefault(p => p.Id == selection);

                        int quantity = int.Parse(Console.ReadLine() ?? "0");
                        if(quantity > 0 &&)
                        ShoppingCartServiceProxy.Current.AddOrUpdate(new CartProduct
                        {
                            Id = selectedID.Id,
                            Name = selectedID.Name,
                            Quantity = quantity
                        });
                        break;
                    case 'R':
                    case 'r':
                        list.ForEach(Console.WriteLine);
                        break;
                    case 'U':
                    case 'u':
                        Console.WriteLine("Which product would you like to update?");
                        int selection = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedProd = list.FirstOrDefault(p => p.Id == selection);

                        if (selectedProd != null)
                        {
                            selectedProd.Name = Console.ReadLine() ?? "ERROR";
                            ShoppingCartServiceProxy.Current.AddOrUpdate(selectedProd);
                        }
                        break;
                    case 'D':
                    case 'd':
                        Console.WriteLine("Which product would you like to update?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        ShoppingCartServiceProxy.Current.Delete(selection);
                        break;
                    case 'Q':
                    case 'q':
                        break;
                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }
            } while (cartChoice != 'Q' && cartChoice != 'q');

            Console.ReadLine();
        }
    }
}
