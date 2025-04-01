using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.DTO;

namespace ced22b_cop4870_project1.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public int Quantity { get; set; } = 1;
        
        public string? Display
        {
            get
             {
                return $"{Id}. {Name}: {Price,4:C}     Quantity: {Quantity,1}";
            }
        }

        public string LegacyProperty1 { get; set; }
        public string LegacyProperty2 { get; set; }
        public string LegacyProperty3 {  get; set; }
        public string LegacyProperty4 { get; set; }
        public string LegacyProperty5 { get; set; }
        public string LegacyProperty6 { get; set; }

        public Product()
        {
            Name = string.Empty;
        }

        public Product(Product p)
        {
            Name = p.Name;
            Id = p.Id;
        }

        public override string ToString()
        { 
            return Display ?? string.Empty;
        }

        public Product(ProductDTO p)
        {
            Name = p.Name;
            Id = p.Id;
            LegacyProperty1 = string.Empty;
        }
    }
}
