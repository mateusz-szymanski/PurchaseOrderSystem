using PurchaseOrderSystem.Domain.Orders.DataPackage;
using System;

namespace PurchaseOrderSystem.Domain.Orders
{
    public record Product
    {
        public string Name { get; private set; }
        public int Quantity { get; private set; }

        protected Product(string name, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product must have a name.");

            if (quantity <= 0)
                throw new ArgumentException("Product quantity must be greater than 0.");

            Name = name;
            Quantity = quantity;
        }

        public static Product Create(ProductData data)
        {
            return new Product(data.Name, data.Quantity);
        }
    }
}
