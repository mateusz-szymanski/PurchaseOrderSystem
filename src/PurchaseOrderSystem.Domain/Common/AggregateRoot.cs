using System.Collections.Generic;

namespace PurchaseOrderSystem.Domain.Common
{
    public class AggregateRoot : IAggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public IEnumerable<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents;
        }
    }
}
