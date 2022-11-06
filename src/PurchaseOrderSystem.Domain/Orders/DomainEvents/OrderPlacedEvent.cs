using PurchaseOrderSystem.Domain.Common;
using System;

namespace PurchaseOrderSystem.Domain.Orders.DomainEvents
{
    public class OrderPlacedEvent : IDomainEvent
    {
        public Guid OrderId { get; private set; }

        public OrderPlacedEvent(Order order)
        {
            OrderId = order.Id;
        }
    }
}
