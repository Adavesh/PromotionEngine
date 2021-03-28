using Abrand.PromotionEngine.Models;
using Abrand.PromotionEngine.Services.Contracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Abrand.PromotionEngine.Services
{
    public class InMemoryPromotionsService: Collection<Promotion>, IPromotionsService
    {
        public IDiscountProvider DiscountProvider { get; set; }

        public InMemoryPromotionsService()
        {
            Add(new Promotion()
            {
                PromotionName = "A3",
                PromotionItems = new List<PromotionItem>() { new PromotionItem("A", 3) }
            });

            Add(new Promotion()
            {
                PromotionName = "B2",
                PromotionItems = new List<PromotionItem>() { new PromotionItem("B", 2) }
            });

            Add(new Promotion()
            {
                PromotionName = "C1_D1",
                PromotionItems = new List<PromotionItem>() { new PromotionItem("C", 1), new PromotionItem("D", 1) },
            });
        }
    }
}
