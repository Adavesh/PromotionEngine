namespace Abrand.PromotionEngine.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public decimal Value
        {
            get
            {
                return Product.UnitPrice * Quantity;
            }
        }
    }
}
