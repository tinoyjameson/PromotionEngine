using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngineTest
{
    [TestClass]
    public class OrderTest
    {
        /// <summary>
        /// When Adding new items.
        /// </summary>
        [TestMethod]
        public void AddTest1()
        {
            IOrder order = new Order();
            Product product1 = new Product() { Sku = "A", Price = 10.50 };
            Product product2 = new Product() { Sku = "B", Price = 15.50 };
            order.Add(product1, 2);
            order.Add(product2, 1);
            var count = order.OrderedItems.Count;
            Assert.AreEqual(2, count);
        }
        /// <summary>
        /// When Add or update new items.
        /// </summary>
        [TestMethod]
        public void AddTest2()
        {
            IOrder order = new Order();
            Product product1 = new Product() { Sku = "A", Price = 10.50 };
            Product product2 = new Product() { Sku = "B", Price = 15.50 };
            order.Add(product1, 2);
            order.Add(product1, 3);
            var count = order.OrderedItems.Count;
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void GetProductQuantity()
        {
            IOrder order = new Order();
            Product product1 = new Product() { Sku = "A", Price = 10.50 };
            Product product2 = new Product() { Sku = "B", Price = 15.50 };
            order.Add(product1, 2);

            var quantity = order.GetProductQuantity(product1);
            Assert.AreEqual(2, quantity);
        }
    }
}
