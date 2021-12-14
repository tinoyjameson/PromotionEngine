using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    public class PromotionEngine: IPromotionEngine
    {
        public double GetDiscount(IOrder order, List<IPromotion> promotions)
        {
            double discount = 0;
            var discountedCart = order.Clone();
            foreach(var promotion in promotions)
            {
                if (promotion.IsApplicable(order))
                {
                    var promotionResult = promotion.CalculateDiscount(discountedCart);
                    discount = discount + promotionResult.Item2;
                    discountedCart = promotionResult.Item1 ;
                }
            }
            return discount;
        }
    }
}
