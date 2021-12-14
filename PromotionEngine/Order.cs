using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    /// <summary>
    /// Order class to store the order from customer.
    /// </summary>
    public class Order: IOrder
    {
        private Dictionary<Product, int> orderedItems;

        public Order()
        {
            orderedItems = new Dictionary<Product, int>();
        }

        public Dictionary<Product, int> OrderedItems
        {
            get
            {
                return orderedItems;
            }
        }

        /// <summary>
        /// Add product or update quantity to existing product.
        /// </summary>
        /// <param name="product">Product object</param>
        /// <param name="quantity">quantity in numbers</param>
        public void Add(Product product, int quantity)
        {
            int newQuantity = quantity;
            if(this.orderedItems.ContainsKey(product)){
                var oldQuantity = this.orderedItems[product];
                newQuantity = oldQuantity + quantity;
                this.orderedItems[product] = newQuantity;
            }
            else
            {
                this.orderedItems.Add(product, newQuantity);
            }
            
        }

        public void Remove(Product product, int quantity)
        {
            if (this.orderedItems.ContainsKey(product))
            {
                var oldQuantity = this.orderedItems[product];
                this.orderedItems[product] = (oldQuantity - quantity > 0) ? oldQuantity - quantity : 0;
            }
        }

        /// <summary>
        /// Update product quantity.
        /// purpose is to manage the cart while applying promotions.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public void UpdateProductQuantity(Product product, int quantity)
        {
            if(quantity > 0)
            {
                this.orderedItems[product] = quantity;
            }
            else
            {
                this.orderedItems.Remove(product);
            }
        }

        public int GetProductQuantity(Product product)
        {
            if (this.orderedItems.ContainsKey(product))
            {
                return this.orderedItems[product];
            }
            return 0;
        }

        public bool HasProduct(Product product)
        {
            return this.orderedItems.ContainsKey(product);
        }

        public IOrder Clone()
        {
            var order = new Order();
            foreach(var orderedItem in this.orderedItems)
            {
                order.Add(orderedItem.Key, orderedItem.Value);
            }
            return order;
        }

        public int GetTotalCount()
        {
            var totalCount = 0;
            foreach(var orderedItem in this.orderedItems)
            {
                totalCount = totalCount + orderedItem.Value;
            }
            return totalCount;
        }

        public int GetTotalItems()
        {
            return orderedItems.Count;
        }
    }
}
