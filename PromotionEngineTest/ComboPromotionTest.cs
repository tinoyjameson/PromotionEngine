using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngineTest
{
    [TestClass]
    public class ComboPromotionTest
    {
        /// <summary>
        /// Calculate discount when order with valid combo promotion.
        /// </summary>
        [TestMethod]
        public void CalculateDiscountCase1()
        {
            var product1 = new Product() { Sku = "A", Price = 20 };
            var product2 = new Product() { Sku = "B", Price = 30 };
            var discountItems = new Dictionary<Product, int>();
            discountItems.Add(product1, 1);
            discountItems.Add(product2, 1);
            var promotion = new ComboPromotion(discountItems, 40);

            var order = new Order();
            order.Add(product1, 3);
            order.Add(product2, 3);
            var output = promotion.CalculateDiscount(order);
            Assert.AreEqual(output.Item2, 30);
        }

        /// <summary>
        /// Calculate discount when order with there is no valid combo promotion.
        /// </summary>
        [TestMethod]
        public void CalculateDiscountCase2()
        {
            var product1 = new Product() { Sku = "A", Price = 20 };
            var product2 = new Product() { Sku = "B", Price = 30 };
            var product3 = new Product() { Sku = "C", Price = 10 };
            var discountItems = new Dictionary<Product, int>();
            discountItems.Add(product1, 2);
            discountItems.Add(product2, 2);
            var promotion = new ComboPromotion(discountItems, 40);

            var order = new Order();
            order.Add(product1, 1);
            order.Add(product2, 1);
            var output = promotion.CalculateDiscount(order);
            Assert.AreEqual(output.Item2, 0);
        }
    }
}
