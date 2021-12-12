using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public class PromotionRepository
    {
        private List<IPromotion> promotions;
        public PromotionRepository()
        {
            this.promotions = new List<IPromotion>();
        }

        public void addPromotion(IPromotion promotion)
        {
            this.promotions.Add(promotion);
        }

        public void executePromotion(IPromotion promotion)
        {

        }
    }
}
