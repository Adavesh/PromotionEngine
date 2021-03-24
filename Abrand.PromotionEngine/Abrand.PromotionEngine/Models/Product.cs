using System;
using System.Collections.Generic;
using System.Text;

namespace Abrand.PromotionEngine.Models
{
    public class Product
    {
        public string Sku { get; set; }

        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public Product() { }

        public Product(string sku, string name, decimal unitPrice)
        {
            Sku = sku;
            Name = name;
            UnitPrice = unitPrice;
        }
    }
}
