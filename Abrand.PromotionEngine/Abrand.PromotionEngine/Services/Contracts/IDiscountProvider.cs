using Abrand.PromotionEngine.Models;
using System;
using System.Collections.Generic;

namespace Abrand.PromotionEngine.Services.Contracts
{
    public interface IDiscountProvider
    {
        decimal ApplyPromotion(Cart cart, Func<ICollection<Promotion>, Promotion> promoSelector);
    }
}
