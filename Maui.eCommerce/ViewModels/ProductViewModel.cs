﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ced22b_cop4870_project1.Models;
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels
{
    public class ProductViewModel
    {
        private Item? cachedModel {  get; set; }
        public string? Name
        {
            get
            {
                return Model?.Product?.Name ?? string.Empty;
            }

            set
            {
                if (Model != null && Model.Product?.Name != value)
                {
                    Model.Product.Name = value;
                }
            }
        }

        public int? Quantity
        {
            get
            {
                return Model?.Quantity;
            }

            set
            {
                if(Model != null && value != null && Model.Quantity != value)
                {
                    Model.Quantity = value;
                }
            }
        }

        public decimal? Price
        {
            get
            {
                return Model?.Product?.Price;
            }

            set
            {
                if(Model?.Product != null && Model.Product.Price != value)
                {
                    Model.Product.Price = value;
                }
            }
        }

        public Item? Model { get; set; }

        public void AddOrUpdate()
        {
            ProductServiceProxy.Current.AddOrUpdate(Model);
        }

        public void Undo()
        {
            ProductServiceProxy.Current.AddOrUpdate(cachedModel);
        }

        public ProductViewModel() 
        {
            Model = new Item();
            cachedModel = null;
        }

        public ProductViewModel(Item? model)
        {
            Model = model;
            if(model != null)
            {
                cachedModel = new Item(model);
            }
        }
    }
}
