using Abrand.PromotionEngine.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Abrand.PromotionEngine.Services
{
    class InMemoryProductsService : Collection<Product>, IProductsService
    {
        public InMemoryProductsService()
        {
            Add(new Product("A", "ProductA", 50.00M));
            Add(new Product("B", "ProductB", 30.00M));
            Add(new Product("C", "ProductC", 20.00M));
            Add(new Product("D", "ProductD", 15.00M));
        }

        public bool AddProduct(string sku, string name, decimal unitPrice)
        {
            if(this.Any(p => p.Sku == sku))
            {
                return false;
            }

            Add(new Product(sku, name, unitPrice));

            return true;
        }

        public Product GetProduct(string Sku)
        {
            return this.Where(product => product.Sku == Sku)
                .Select(p => new Product(p.Sku, p.Name, p.UnitPrice))
                .SingleOrDefault();
        }

        public IList<Product> GetProducts()
        {
            return this.Select(p => new Product(p.Sku, p.Name, p.UnitPrice)).ToList();
        }

        public bool RemoveProduct(string sku)
        {
            if (this.SingleOrDefault(p => p.Sku == sku) is Product product)
            {
                Remove(product);
                return true;
            }

            return false;
        }
    }
}
