using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Abrand.PromotionEngine.Models
{
    public class Cart : Collection<CartItem>
    {
        public decimal TotalValue
        {
            get
            {
                return this.Sum(item => item.Value);
            }
        }
    }
}