using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public interface IOrder
    {
        Dictionary<Product, int> OrderedItems { get; }
        void AddOrUpdate(Product product, int quantity);
        bool HasProduct(Product product);
        void Remove(Product product, int quantity);
        int GetProductQuantity(Product product);
        void UpdateProductQuantity(Product product, int quantity);
        void UpdateProductPrice(Product product, double price);
        IOrder Clone();
    }
}
