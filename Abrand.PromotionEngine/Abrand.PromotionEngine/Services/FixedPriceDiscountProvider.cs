using Abrand.PromotionEngine.Models;
using Abrand.PromotionEngine.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abrand.PromotionEngine.Services
{
    public class FixedPriceDiscountProvider : IDiscountProvider
    {
        private readonly IPromotionsService _promotions;
        private readonly Dictionary<string, decimal> _discountsSchemes;

        public FixedPriceDiscountProvider(IPromotionsService promotions)
        {
            _promotions = promotions;
            _discountsSchemes = new Dictionary<string, decimal>();
            LoadDiscountInfo();
        }

        /// <summary>
        /// Applies any applicable promotion on the given cart.
        /// </summary>
        /// <param name="cart"><see cref="Cart"/> to be promoted</param>
        /// <param name="promoSelector">Callback to select single promotion from one or more applicable promotions</param>
        /// <returns>Promoted cart value</returns>
        public decimal ApplyPromotion(Cart cart, Func<ICollection<Promotion>, Promotion> promoSelector)
        {
            var cartValue = 0.0M;
            Promotion selectedPromotion = null;

            if (promoSelector != null)
            {
                var applicablePromotions = GetApplicablePromotions(cart);
                selectedPromotion = promoSelector(applicablePromotions);
            }

            if(_discountsSchemes.TryGetValue(selectedPromotion?.PromotionName, out decimal promoDiscount))
            {
                var applyCount = GetApplyCount(cart, selectedPromotion);
                cartValue = promoDiscount * applyCount;

                foreach (var item in cart)
                {
                    var promoItem = selectedPromotion.PromotionItems.FirstOrDefault(p => p.Sku == item.Product.Sku);

                    if (promoItem == null)
                    {
                        cartValue += item.Product.UnitPrice * item.Quantity;
                    }
                    else
                    {
                        cartValue += item.Product.UnitPrice * (item.Quantity - (promoItem.Quantity * applyCount));
                    }
                }
            }
            else
            {
                cartValue = cart.Sum(item => item.Product.UnitPrice * item.Quantity);
            }

            return cartValue;
        }

        /// <summary>
        /// This method gets all the available promotions that can be applied to the given cart.
        /// </summary>
        private List<Promotion> GetApplicablePromotions(Cart cart)
        {
            var applicablePromotions = new List<Promotion>();

            foreach (var promo in _promotions)
            {
                var applicable = true;

                foreach (var promoItem in promo.PromotionItems)
                {
                    if (cart.Any(cartItem => cartItem.Product.Sku == promoItem.Sku && cartItem.Quantity >= promoItem.Quantity))
                    {
                        continue;
                    }

                    applicable = false;
                }

                if (applicable)
                {
                    applicablePromotions.Add(promo);
                }
            }

            return applicablePromotions;
        }

        /// <summary>
        /// This  method gets how many times the selected promotion can be applied on the cart.
        /// For example, say the promotion is 3 A's for 130 and there are 10 A's, then the promotion is applicable 3 times.
        /// </summary>
        private int GetApplyCount(Cart cart, Promotion promotion)
        {
            var applyCount = 0;

            while (applyCount < ++applyCount)
            {
                foreach (var promotionItem in promotion.PromotionItems)
                {
                    var cartItem = cart.FirstOrDefault(ci => ci.Product.Sku == promotionItem.Sku);

                    if (cartItem == null)
                    {
                        continue;
                    }

                    var remainingQuantity = cartItem.Quantity - promotionItem.Quantity * applyCount;

                    if (remainingQuantity < promotionItem.Quantity)
                    {
                        return applyCount;
                    }
                }
            }

            return applyCount - 1;
        }

        /// <summary>
        /// In memory promo and their discount values
        /// </summary>
        private void LoadDiscountInfo()
        {
            _discountsSchemes.Add("A3", 130);
            _discountsSchemes.Add("B2", 45);
            _discountsSchemes.Add("C1_D1", 30);
        }
    }
}
