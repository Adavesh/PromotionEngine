using System.Collections.Generic;

namespace Abrand.PromotionEngine.Models
{
    public class Promotion
    {
        public string PromotionName{ get; set; }

        public IList<PromotionItem> PromotionItems { get; set; }
    }
}