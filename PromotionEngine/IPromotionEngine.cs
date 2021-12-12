﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public interface IPromotionEngine 
    {
        double GetOfferPrice(IOrder order, IPromotion[] promotions);
    }
}
