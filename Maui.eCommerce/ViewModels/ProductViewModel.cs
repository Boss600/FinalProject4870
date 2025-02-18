using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ced22b_cop4870_project1.Models;

namespace Maui.eCommerce.ViewModels
{
    public class ProductViewModel
    {
        public string? Name
        {
            get
            {
                return Model?.Name ?? string.Empty;
            }

            set
            {
                if (Model != null && Model.Name != value)
                {
                    Model.Name = value;
                }
            }
        }

        public Product? Model { get; set; }

        public ProductViewModel() 
        {
            Model = new Product();
        }

        public ProductViewModel(Product? model)
        {
            Model = model;
        }
    }
}
