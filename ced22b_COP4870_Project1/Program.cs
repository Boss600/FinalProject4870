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
            char choice;
            do
            {
                Menu();
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
                        Checkout();
                        break;
                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }
            } while (choice != '3');
        }

        static void Menu()
        {
            Console.WriteLine("Welcome to Amazon!");

            Console.WriteLine("1. Manage inventory");
            Console.WriteLine("2. Manage shopping cart");
            Console.WriteLine("3. Checkout");
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
                        Console.WriteLine("New Product Name: ");
                        string productName = Console.ReadLine() ?? "Unnamed Product";

                        Console.WriteLine("New Product Price: ");
                        if(!double.TryParse(Console.ReadLine(), out double newPrice))
                        {
                            Console.WriteLine("Setting price to default $0.00");
                            newPrice = 0.00;
                        }

                        double productPrice = newPrice;

                        Console.WriteLine("Quantity: ");
                        if(!int.TryParse(Console.ReadLine(), out int newQuantity))
                        {
                            Console.WriteLine("Setting quantity to default 1 unit");
                            newQuantity = 1;
                        }

                        int productQuantity = newQuantity;

                        ProductServiceProxy.Current.AddOrUpdate(new Product
                        {
                            Name = productName,
                            Price = productPrice,
                            Quantity = productQuantity
                        });
                        break;
                    case 'R':
                    case 'r':
                        list.ForEach(Console.WriteLine);
                        break;
                    case 'U':
                    case 'u':
                        Console.WriteLine("Which product would you like to update(ID)?");
                        int selection = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedProd = list.FirstOrDefault(p => p.Id == selection);

                        if (selectedProd != null)
                        {
                            Console.WriteLine("Enter new product name: ");
                            selectedProd.Name = Console.ReadLine() ?? "ERROR";

                            Console.WriteLine("Enter new product price: ");
                            if(!double.TryParse(Console.ReadLine(), out newPrice))
                            {
                                Console.WriteLine("Setting price to default $0.00");
                                newPrice = 0.00;
                            }

                            selectedProd.Price = newPrice;

                            Console.WriteLine("Enter new product quantity: ");
                            if(!int.TryParse(Console.ReadLine(), out newQuantity)){
                                Console.WriteLine("Setting quantity to default 1 unit");
                                newQuantity = 1;
                            }

                            selectedProd.Quantity = newQuantity;

                            ProductServiceProxy.Current.AddOrUpdate(selectedProd);
                        }
                        break;
                    case 'D':
                    case 'd':
                        Console.WriteLine("Which product would you like to delete(ID)?");
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
            Console.WriteLine("R. Read shopping cart items");
            Console.WriteLine("U. Update shopping cart item");
            Console.WriteLine("D. Delete a shopping cart item");
            Console.WriteLine("Q. Quit");

            List<CartProduct?> cart = ShoppingCartServiceProxy.Current.CartProducts;
            List<Product?> inventory = ProductServiceProxy.Current.Products;

            char cartChoice;
            do
            {
                string? input = Console.ReadLine();
                cartChoice = input[0];
                switch (cartChoice)
                {
                    case 'A':
                    case 'a':
                        Console.WriteLine("Which product would you like to add to your cart(ID)?");
                        if (!int.TryParse(Console.ReadLine(), out int productId))
                        {
                            Console.WriteLine("Invalid Input");
                            break;
                        }

                        var selectedProduct = inventory.FirstOrDefault(p => p.Id == productId);
                        if (selectedProduct == null)
                        {
                            Console.WriteLine("Error: Product not found in inventory.");
                            break;
                        }

                        Console.Write($"Enter Quantity (Available: {selectedProduct.Quantity}): ");
                        if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                        {
                            Console.WriteLine("Invalid quantity");
                            break;
                        }

                        if (quantity > selectedProduct.Quantity)
                        {
                            Console.WriteLine("Error: Not enough stock available.");
                            break;
                        }

                        var cartProducts = cart.FirstOrDefault(p => p.Id == productId);
                        if (cartProducts != null)
                        {
                            cartProducts.Quantity += quantity;
                        } 
                        else
                        {
                            cartProducts = new CartProduct
                            {
                                Id = selectedProduct.Id,
                                Name = selectedProduct.Name,
                                Quantity = quantity,
                                Price = (double)selectedProduct.Price
                            };
                            cart.Add(cartProducts);
                        }

                        selectedProduct.Quantity -= quantity;
                        Console.WriteLine($"Updated inventory: {selectedProduct.Name} now has {selectedProduct.Quantity} left.");
                        break;
                    case 'R':
                    case 'r':
                        if(cart.Count == 0)
                            Console.WriteLine("Empty Shopping Cart");

                        Console.WriteLine("Shopping Cart: ");
                        cart.ForEach(Console.WriteLine);
                        break;
                    case 'U':
                    case 'u':
                        Console.WriteLine("Which product would you like to update(ID)?");
                        if (!int.TryParse(Console.ReadLine(), out int updateId))
                        {
                            Console.WriteLine("Invalid input.");
                            break;
                        }

                        var cartProduct = cart.FirstOrDefault(p => p.Id == updateId);
                        if (cartProduct == null)
                        {
                            Console.WriteLine("Error: Product not in cart");
                            break;
                        }

                        Console.Write($"Enter Quantity (Available: {cartProduct.Quantity}): ");
                        if (!int.TryParse(Console.ReadLine(), out int newQuantity) || newQuantity <= 0)
                        {
                            Console.WriteLine("Invalid quantity");
                            break;
                        }

                        var inventoryItem = inventory.FirstOrDefault(p => p.Id == updateId);
                        if (inventoryItem != null)
                        {
                            if (newQuantity > inventoryItem.Quantity + cartProduct.Quantity)
                            {
                                Console.WriteLine($"Error: Cannot set quantity higher than available inventory ({inventoryItem.Quantity + cartProduct.Quantity}).");
                                break;
                            }
                            inventoryItem.Quantity += cartProduct.Quantity;
                            inventoryItem.Quantity -= newQuantity;
                        }

                        cartProduct.Quantity = newQuantity;
                        ShoppingCartServiceProxy.Current.AddOrUpdate(cartProduct);
                        Console.WriteLine($"Updated quantity of {cartProduct.Name} in cart to {newQuantity}");
                        break;
                    case 'D':
                    case 'd':
                        Console.WriteLine("Which product would you like to delete from the cart(ID)?");
                        if (!int.TryParse(Console.ReadLine(), out productId))
                        {
                            Console.WriteLine("Invalid input.");
                            break;
                        }
                        ShoppingCartServiceProxy.Current.Delete(productId);
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

        static void Checkout()
        {
            Console.WriteLine(" =-=-=-=-=> C H E C K O U T <=-=-=-=-= ");
            var cart = ShoppingCartServiceProxy.Current.CartProducts;
            double total = cart.Sum(p => p.TotalPrice);
            Console.WriteLine("Items in Cart:");
            cart.ForEach(Console.WriteLine);
            total = total * 1.07;
            Console.WriteLine($"Total Amount Due with 7% Sales Tax: ${total} ");
            Console.WriteLine("Thank you for Shopping at Amazon");
        }
    }

}

// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
//                                                        E N D    O F   F I L E
// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=