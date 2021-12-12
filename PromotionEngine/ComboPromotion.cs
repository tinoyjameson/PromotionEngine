using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace PromotionEngine
{
    public class BulkPromotion : IPromotion
    {
        private Dictionary<Product, int> requiredItems;
        private double OfferPrice;
        public BulkPromotion(KeyValuePair<Product,int> items, double price)
        {
            this.requiredItems = new Dictionary<Product, int>();
            requiredItems.Add(items.Key, items.Value);
            this.OfferPrice = price;
        }
        
        public void AddRequiredItem(Product product, int quantity)
        {
            this.requiredItems.Add(product, quantity);
        }

        private int GetRequiredQuantity(Product product)
        {
            return this.requiredItems[product];
        }

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

        public Tuple<IOrder, double> CalculateDiscount(IOrder order)
        {
            if (!this.IsApplicable(order))
            {
                return Tuple.Create(order, 0.0);
            }
            double oldPrice = 0;
            int counter = 0;
            foreach (var requiredItem in this.requiredItems)
            {
                counter++;
                var requiredQuantity = requiredItem.Value;
                var totalPrice = requiredItem.Key.Price * requiredQuantity;
                oldPrice = oldPrice + totalPrice;
                order.UpdateProductQuantity(requiredItem.Key, requiredQuantity);
                if (counter < this.requiredItems.Count)
                {
                    order.UpdateProductPrice(requiredItem.Key, 0);
                }
                else
                {
                    order.UpdateProductPrice(requiredItem.Key, this.OfferPrice);
                }
            }
            double discount = oldPrice - this.OfferPrice;
            return Tuple.Create(order, discount);
        }
    }
}
