using PurchaseOrderSystem.Domain.Common;
using PurchaseOrderSystem.Domain.Orders.DataPackage;
using PurchaseOrderSystem.Domain.Orders.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PurchaseOrderSystem.Domain.Orders
{
    public class Order : AggregateRoot
    {
        public Guid Id { get; private set; }

        public string Email { get; private set; }
        public decimal Amount { get; private set; }
        public Address Address { get; private set; }
        public IEnumerable<Product> Products { get; private set; }

        public DateTime PlacedAt { get; private set; }

        protected Order(string email, decimal amount, Address address, IEnumerable<Product> products)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email must be specified.");

            if (amount < 0)
                throw new ArgumentException("Order amount cannot be less than 0.");

            if (!products.Any())
                throw new ArgumentException("There must be at least one product in the order.");

            Id = Guid.NewGuid();
            PlacedAt = DateTime.UtcNow;
            Email = email;
            Amount = amount;
            Address = address;
            Products = products.ToList();

            AddDomainEvent(new OrderPlacedEvent(this));
        }

        public static Order Create(OrderData data)
        {
            var address = Address.Create(data.Address);
            var products = data.Products.Select(p => Product.Create(p));
            var order = new Order(data.Email, data.Amount, address, products);

            return order;
        }
    }
}
