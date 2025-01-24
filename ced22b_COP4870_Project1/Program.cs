using ced22b_cop4870_project1.Models;
using Library.eCommerce.Services;

namespace ced22b_cop4870_project1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var lastKey = 1;
            Console.WriteLine("Welcome to Amazon!");

            Console.WriteLine("C. Create new inventory item");
            Console.WriteLine("R. Read new inventory item");
            Console.WriteLine("U. Update an inventory item");
            Console.WriteLine("D. Delete an inventory item");
            Console.WriteLine("Q. Quit");

            List<Product?> list = ProductServiceProxy.Current.Products;

            char choice;
            do
            {
                string? input = Console.ReadLine();
                choice = input[0];
                switch (choice)
                {
                    case 'C':
                    case 'c':
                        list.Add(new Product
                        {
                            Id = lastKey++,
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
                        }
                        break;
                    case 'D':
                    case 'd':
                        Console.WriteLine("Which product would you like to update?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        selectedProd = list.FirstOrDefault(p => p.Id == selection);
                        list.Remove(selectedProd);
                        break;
                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }
            } while (choice != 'Q' && choice != 'q');

            Console.ReadLine();
        }
        static void AddProduct(List<string?> list)
        {
            var newProduct = Console.ReadLine() ?? "UNK";
            list.Add(newProduct);
        }
    }
}
