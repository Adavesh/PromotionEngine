using Abrand.PromotionEngine.Models;
using Abrand.PromotionEngine.Services.Contracts;
using System.Collections.Generic;

namespace Abrand.PromotionEngine.Services
{
    public class UnitPriceDiscountProvider : IDiscountProvider
    {
        private readonly Dictionary<string, decimal> _discountSchemes;
        private readonly IProductsService _products;

        public UnitPriceDiscountProvider(IProductsService products)
        {
            _products = products;
            _discountSchemes = new Dictionary<string, decimal>();
            LoadDiscountSchemes();
        }

        private void LoadDiscountSchemes()
        {
            _discountSchemes.Add("A", 5);
            _discountSchemes.Add("B", 10);
            _discountSchemes.Add("C", 10);
            _discountSchemes.Add("D", 25);
        }

        public decimal ApplyPromotion(Cart cart, System.Func<ICollection<Promotion>, Promotion> promoSelector)
        {
            throw new System.NotImplementedException();
        }
    }
}
