﻿namespace Abrand.PromotionEngine.Models
{
    public class PromotionItem
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }

        public PromotionItem(string sku, int qty)
        {
            Sku = sku;
            Quantity = qty;
        }
    }
}
