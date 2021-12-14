using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    /// <summary>
    /// Calculate Price of order.
    /// </summary>
    public class PriceCalculator : IPriceCalculator
    {
        /// <summary>
        /// Get total price in order basket.
        /// </summary>
        /// <param name="order">Order product</param>
        /// <returns>returns total price </returns>
        public double GetTotalPrice(IOrder order)
        {
            double totalPrice = 0;
            foreach (var orderedItem in order.OrderedItems)
            {
                totalPrice = totalPrice + orderedItem.Key.Price * orderedItem.Value;
            }
            return totalPrice;
        }
  }
}

