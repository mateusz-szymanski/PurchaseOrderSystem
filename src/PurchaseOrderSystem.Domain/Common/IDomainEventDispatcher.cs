using System.Collections.Generic;

namespace PurchaseOrderSystem.Domain.Common
{
    public interface IDomainEventDispatcher
    {
        void AddDomainEvent(IDomainEvent domainEvent);
        void AddDomainEvent(IAggregateRoot aggregateRoot);
        IEnumerable<IDomainEvent> GetAllEvents();
    }
}
