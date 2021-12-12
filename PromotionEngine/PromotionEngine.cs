using System;

namespace PromotionEngine
{
    public class PromotionEngine: IPromotionEngine
    {
        public double GetOfferPrice(IOrder order, IPromotion[] promotions)
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
