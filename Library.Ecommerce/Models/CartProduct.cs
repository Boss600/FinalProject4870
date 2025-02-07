using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ced22b_cop4870_project1.Models
{
    public class CartProduct
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice => Price * Quantity;

        public string? Display
        {
            get
            {
                return $"{Id}. {Name}: {Price,4:C}     Quantity: {Quantity,1}";
            }
        }

        public CartProduct()
        {
            Name = string.Empty;
            Price = 0.0;
            Quantity = 0;
        }

        public override string ToString()
        {
            return Display ?? string.Empty;
        }
    }
}
