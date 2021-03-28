using Abrand.PromotionEngine.Models;
using Abrand.PromotionEngine.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abrand.PromotionEngine
{
    public class CartManager
    {
        private readonly IDiscountProvider _discountProvider;
        private readonly Cart _cart;

        public CartManager(IDiscountProvider discountProvider)
        {
            _cart = new Cart();
            _discountProvider = discountProvider;
        }

        public void Add(Product product, int quantity)
        {
            _cart.Add(new CartItem(product, quantity));
        }

        public void Update(Product product, int quantity)
        {
            var cartItem = _cart.FirstOrDefault(ci => ci.Product.Sku == product.Sku);
            cartItem.Quantity = quantity;
        }

        public void Remove(Product product)
        {
            var cartItem = _cart.FirstOrDefault(ci => ci.Product.Sku == product.Sku);
            if (cartItem != null)
            {
                _cart.Remove(cartItem);
            }
        }

        public decimal ApplyPromotion(Func<ICollection<Promotion>,Promotion> promotionSelector)
        {
            return _discountProvider.ApplyPromotion(_cart, promotionSelector);
        }
    }
}