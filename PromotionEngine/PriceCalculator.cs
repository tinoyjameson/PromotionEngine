using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public class PriceCalculator : IPriceCalculator
    {
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

