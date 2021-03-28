using Abrand.PromotionEngine.Models;
using Abrand.PromotionEngine.Services;
using Abrand.PromotionEngine.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abrand.PromotionEngine.Tests
{
    [TestClass]
    public class CartManagerTest
    {
        private IPromotionsService _promotions;
        private IDiscountProvider _discountProvider;

        private CartManager _cartManager;

        [TestInitialize]
        public void Init()
        {
            _promotions = new InMemoryPromotionsService();
            _discountProvider = new FixedPriceDiscountProvider(_promotions);
            _cartManager = new CartManager(_discountProvider);
        }

        [TestMethod]
        public void ApplyPromotion_FixedPrice_Test1()
        {            
            _cartManager.Add(new Product("A", "Product A", 50.0M), 5);
            _cartManager.Add(new Product("B", "Product B", 30.0M), 5);
            _cartManager.Add(new Product("C", "Product C", 20.0M), 1);
            _cartManager.Add(new Product("D", "Product D", 15.0M), 1);

            var promotionSelector = new Func<ICollection<Promotion>, Promotion>(promotions =>
            {
                //Selecting first available promotion which is 3 A's for 130
                return promotions.FirstOrDefault();
            });

            Assert.AreEqual(_cartManager.ApplyPromotion(promotionSelector), 130 + 100 + 150 + 35);
        }

        [TestMethod]
        public void ApplyPromotion_FixedPrice_Test2()
        {
            _cartManager.Add(new Product("A", "Product A", 50.0M), 5);
            _cartManager.Add(new Product("B", "Product B", 30.0M), 5);
            _cartManager.Add(new Product("C", "Product C", 20.0M), 1);
            _cartManager.Add(new Product("D", "Product D", 15.0M), 1);

            var promotionSelector = new Func<ICollection<Promotion>, Promotion>(promotions =>
            {
                //Selecting first available promotion which contains Product B
                return promotions.FirstOrDefault(p => p.PromotionItems.Any(pi => pi.Sku == "B"));
            });

            Assert.AreEqual(_cartManager.ApplyPromotion(promotionSelector), 250 + 120 + 35);
        }

        [TestMethod]
        public void ApplyPromotion_FixedPrice_Test3()
        {
            _cartManager.Add(new Product("A", "Product A", 50.0M), 5);
            _cartManager.Add(new Product("B", "Product B", 30.0M), 5);
            _cartManager.Add(new Product("C", "Product C", 20.0M), 1);
            _cartManager.Add(new Product("D", "Product D", 15.0M), 1);

            var promotionSelector = new Func<ICollection<Promotion>, Promotion>(promotions =>
            {
                //Selecting first available promotion which contains Product C
                return promotions.FirstOrDefault(p => p.PromotionItems.Any(pi => pi.Sku == "C"));
            });

            Assert.AreEqual(_cartManager.ApplyPromotion(promotionSelector), 250 + 150 + 30);
        }

        [TestMethod]
        public void ApplyPromotion_FixedPrice_Test4()
        {
            _cartManager.Add(new Product("A", "Product A", 50.0M), 5);
            _cartManager.Add(new Product("B", "Product B", 30.0M), 5);
            _cartManager.Add(new Product("C", "Product C", 20.0M), 1);
            _cartManager.Add(new Product("D", "Product D", 15.0M), 1);

            var promotionSelector = new Func<ICollection<Promotion>, Promotion>(promotions =>
            {
                //Don't select any promotion
                return promotions.FirstOrDefault(p => p.PromotionItems.Any(pi => pi.Sku == "C"));
            });

            Assert.AreEqual(_cartManager.ApplyPromotion(promotionSelector), 250 + 150 + 30);
        }
    }
}
