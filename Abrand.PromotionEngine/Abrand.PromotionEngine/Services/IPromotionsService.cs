using Abrand.PromotionEngine.Models;
using System.Collections.Generic;

namespace Abrand.PromotionEngine.Services
{
    public interface IPromotionsService
    {
        IList<Promotion> GetPromotions();
        void AddPromotion(decimal discount, DiscountType discountType, params PromotionItem[] items);
        void RemovePromotion(int promotionId);
    }
}
