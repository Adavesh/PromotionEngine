using Abrand.PromotionEngine.Models;
using System.Collections.Generic;

namespace Abrand.PromotionEngine.Services
{
    public interface IProductsService
    {
        IList<Product> GetProducts();
        Product GetProduct(string Sku);
        bool AddProduct(string sku, string, decimal unitPrice);
        bool RemoveProduct(string sku);
    }
}
