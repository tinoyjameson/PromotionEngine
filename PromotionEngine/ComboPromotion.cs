using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace PromotionEngine
{
    /// <summary>
    /// Promotion if order has multiple product with required quantity.
    /// </summary>
    public class ComboPromotion : IPromotion
    {
        private Dictionary<Product, int> requiredItems;
        private double OfferPrice;
        public ComboPromotion(Dictionary<Product, int> items, double price)
        {
            this.requiredItems = items;
            this.OfferPrice = price;
        }
        
        /// <summary>
        /// To add product and requirmed items.
        /// </summary>
        /// <param name="product">Product object</param>
        /// <param name="quantity">quantity in numbers</param>
        public void AddRequiredItem(Product product, int quantity)
        {
            this.requiredItems.Add(product, quantity);
        }
        /// <summary>
        /// To find the required quantity for promotion
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Quantity {int}</returns>
        private int GetRequiredQuantity(Product product)
        {
            return this.requiredItems[product];
        }
        /// <summary>
        /// To check this promotion is applicable to the given order.
        /// </summary>
        /// <param name="order">Order object</param>
        /// <returns></returns>
        public bool IsApplicable(IOrder order)
        {
            foreach(KeyValuePair<Product, int> item in requiredItems)
            {
                var requiredQuantity = this.GetRequiredQuantity(item.Key);
                if (!order.HasProduct(item.Key) || requiredQuantity > order.GetProductQuantity(item.Key))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Calculate discount with combo promotion.
        /// </summary>
        /// <param name="order">Order object</param>
        /// <returns></returns>
        public Tuple<IOrder, double> CalculateDiscount(IOrder order)
        {
            if (!this.IsApplicable(order))
            {
                return Tuple.Create(order, 0.0);
            }
            double oldPrice = 0;
            int noOfBundles = 0;
            while (this.IsApplicable(order))
            {
                noOfBundles++;
                foreach (var requiredItem in this.requiredItems)
                {
                    var requiredQuantity = requiredItem.Value;
                    var totalPrice = requiredItem.Key.Price * requiredQuantity;
                    oldPrice = oldPrice + totalPrice;
                    var totalQuantity = order.OrderedItems[requiredItem.Key];
                    order.UpdateProductQuantity(requiredItem.Key, totalQuantity - 1);
                }
                
            }
            double discount = oldPrice - noOfBundles * this.OfferPrice;
            return Tuple.Create(order, discount);
        }
    }
}
