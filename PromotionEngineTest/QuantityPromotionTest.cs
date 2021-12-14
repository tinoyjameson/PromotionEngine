using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngine;

namespace PromotionEngineTest
{
    [TestClass]
    public class QuantityPromotionTest
    {
        /// <summary>
        /// Calculate Discount when order with valid quantity promotion
        /// </summary>
        [TestMethod]
        public void CalculateDiscountCase1()
        {
            var product1 = new Product() { Sku = "A", Price = 20 };
            var promotion = new QuantityPromotion(product1, 3, 50);

            var order = new Order();
            order.Add(product1, 4);
            var output = promotion.CalculateDiscount(order);
            Assert.AreEqual(output.Item2, 10);
        }

        /// <summary>
        /// Calculate discount when order without valid promotion.
        /// </summary>
        [TestMethod]
        public void CalculateDiscountCase2()
        {
            var product1 = new Product() { Sku = "A", Price = 20 };
            var promotion = new QuantityPromotion(product1, 3, 50);

            var order = new Order();
            order.Add(product1, 2);
            var output = promotion.CalculateDiscount(order);
            Assert.AreEqual(output.Item2, 0);
        }

        [TestMethod]
        public void CalculateDiscountCase3()
        {
            var product1 = new Product() { Sku = "A", Price = 20 };
            var promotion = new QuantityPromotion(product1, 3, 50);

            var order = new Order();
            order.Add(product1, 6);
            var output = promotion.CalculateDiscount(order);
            Assert.AreEqual(output.Item2, 20);
        }
    }
}
