using Abrand.PromotionEngine.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Abrand.PromotionEngine.Services
{
    public class InMemoryPromotions : Collection<Promotion>, IPromotionsService
    {
        public InMemoryPromotions()
        {
            AddPromotion(130, DiscountType.FixedAmount, new PromotionItem("A", 3));
            AddPromotion(45, DiscountType.FixedAmount, new PromotionItem("B", 2));
            AddPromotion(30, DiscountType.FixedAmount, new PromotionItem("C", 1), new PromotionItem("D", 1));
        }

        public void AddPromotion(decimal discount, DiscountType discountType, params PromotionItem[] items)
        {
            var product = new Promotion()
            {
                PromotionId = Count + 1,
                DiscountType = discountType,
                Discount = discount,
                PromotionItems = items
            };

            Add(product);
        }

        public IList<Promotion> GetPromotions()
        {
            return this.Select(p => new Promotion
            {
                PromotionItems = p.PromotionItems,
                Discount = p.Discount,
                DiscountType = p.DiscountType,
                PromotionId = p.PromotionId
            }).ToList();
        }

        public void RemovePromotion(int promotionId)
        {
            if(this.FirstOrDefault(p => p.PromotionId == promotionId) is Promotion promotion)
            {
                Remove(promotion);
            }
        }
    }
}
