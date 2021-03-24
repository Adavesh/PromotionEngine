using System.Collections.Generic;

namespace Abrand.PromotionEngine.Models
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public IList<PromotionItem> PromotionItems { get; set; }
        public decimal Discount { get; set; }
        public DiscountType DiscountType { get; set; }
    }
}
