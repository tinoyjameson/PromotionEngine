using System;

namespace PromotionEngine
{
    /// <summary>
    /// Promotion if one order has a certain required quantity.
    /// </summary>
    public class QuantityPromotion : IPromotion
    {
        private Product requiredProduct;
        private int requiredQuantity;
        private double offerPrice;
        public QuantityPromotion(Product product, int quantity, double price)
        {
            this.requiredProduct = product;
            this.requiredQuantity = quantity;
            this.offerPrice = price;
        }

        /// <summary>
        /// Check to find the quantity promotion is applicable.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool IsApplicable(IOrder order)
        {
            foreach (var item in order.OrderedItems)
            {
                if (item.Key.Sku == this.requiredProduct.Sku && item.Value >= this.requiredQuantity)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Calculate discount with quantity promotion.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Tuple<IOrder, double> CalculateDiscount(IOrder order)
        {
            double discount = 0;
            if (order.OrderedItems.ContainsKey(this.requiredProduct))
            {
                var quantity = order.OrderedItems[this.requiredProduct];
                if(quantity >= this.requiredQuantity)
                {                   
                    var amountOfDiscountBundles = (int)Math.Floor((double)quantity / (double)this.requiredQuantity);
                    var newPrice = amountOfDiscountBundles * this.offerPrice + this.requiredProduct.Price * (quantity - amountOfDiscountBundles * this.requiredQuantity);
                    var oldPrice = quantity * this.requiredProduct.Price;
                    discount = oldPrice - newPrice;
                    order.UpdateProductQuantity(this.requiredProduct, quantity - amountOfDiscountBundles * this.requiredQuantity);
                }
            }

            return Tuple.Create(order, discount);
        }
    }
}
