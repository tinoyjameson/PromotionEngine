using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngine;
using System.Collections.Generic;

namespace PromotionEngineTest
{

    [TestClass]
    public class PromotionEngineTest
    {
        private List<Product> InitializeProducts()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product() { Sku = "A", Price = 50 });
            products.Add(new Product() { Sku = "B", Price = 40 });
            products.Add(new Product() { Sku = "C", Price = 30 });
            products.Add(new Product() { Sku = "D", Price = 15 });
            return products;
        }
        [TestMethod]
        public void PromotionWithQuantityPromotionTest()
        {
            var products = this.InitializeProducts();

            //order
            IOrder order = new Order();
            order.Add(products[0], 5);
            order.Add(products[1], 4);

            // promotions
            var promotions = new List<IPromotion>();
            promotions.Add(new QuantityPromotion(products[0], 3, 130));
            promotions.Add(new QuantityPromotion(products[1], 2, 60));

            IPromotionEngine promotionEngine = new PromotionEngine.PromotionEngine();
            var discount = promotionEngine.GetDiscount(order, promotions);
            Assert.AreEqual(discount, 60);
        }

        [TestMethod]
        public void PromotionWithMultiplePromotionForSameSKUTest()
        {
            var products = this.InitializeProducts();
            //order
            IOrder order = new Order();
            order.Add(products[0], 7);
            order.Add(products[1], 4);

            // promotions
            var promotions = new List<IPromotion>();
            promotions.Add(new QuantityPromotion(products[0], 3, 130));
            promotions.Add(new QuantityPromotion(products[0], 2, 90));
            IPromotionEngine promotionEngine = new PromotionEngine.PromotionEngine();
            var discount = promotionEngine.GetDiscount(order, promotions);
            Assert.AreEqual(discount, 40);
        }

        [TestMethod]
        public void TotalPriceWithPromotionTest()
        {
            var products = this.InitializeProducts();

            //order
            IOrder order = new Order();
            order.Add(products[0], 3);
            order.Add(products[1], 4);
            order.Add(products[2], 2);
            order.Add(products[3], 2);

            // promotions
            var promotions = new List<IPromotion>();
            promotions.Add(new QuantityPromotion(products[0], 3, 130));
            promotions.Add(new QuantityPromotion(products[1], 2, 65));
            var items = new Dictionary<Product, int>();
            items.Add(products[2], 1);
            items.Add(products[3], 1);
            promotions.Add(new ComboPromotion(items, 35));

            IPromotionEngine promotionEngine = new PromotionEngine.PromotionEngine();
            var discount = promotionEngine.GetDiscount(order, promotions);

            IPriceCalculator priceCalculator = new PriceCalculator();
            var totalPrice = priceCalculator.GetTotalPrice(order);
            var priceAfterDiscount = totalPrice - discount;
            Assert.AreEqual(priceAfterDiscount, 330);
        }
    }
}
