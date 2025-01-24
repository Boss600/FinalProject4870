using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ced22b_cop4870_project1.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Display
        {
            get
            {
                return $"{Id}. {Name}";
            }
        }

        public Product()
        {
            Name = string.Empty;
        }

        public override string ToString()
        {
            return Display ?? string.Empty;
        }
    }
}
