using System.Collections.Generic;

namespace PurchaseOrderSystem.Domain.Common
{
    public interface IAggregateRoot
    {
        void AddDomainEvent(IDomainEvent domainEvent);
        IEnumerable<IDomainEvent> GetDomainEvents();
    }
}