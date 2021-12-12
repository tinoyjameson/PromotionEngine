using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public interface IPromotion
    {
        bool IsApplicable(IOrder order);
        Tuple<IOrder, double> CalculateDiscount(IOrder order);
    }
}
