using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
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
        public void AddOrUpdate(Product product, int quantity)
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

        public void UpdateProductPrice(Product product, double price)
        {
            var quantity = this.orderedItems[product];
            this.orderedItems.Remove(product);
            product.Price = price;
            this.orderedItems.Add(product, quantity);
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
                order.AddOrUpdate(orderedItem.Key, orderedItem.Value);
            }
            return order;
        }
    }
}
