using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public interface IPromotionEngine 
    {
        double GetDiscount(IOrder order, List<IPromotion> promotions);
    }
}
