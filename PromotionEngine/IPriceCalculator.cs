using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public interface IPriceCalculator
    {
        public double GetTotalPrice(IOrder order);
    }
}
