using System.Collections.Generic;

namespace PurchaseOrderSystem.Domain.Common
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void AddDomainEvent(IAggregateRoot aggregateRoot)
        {
            _domainEvents.AddRange(aggregateRoot.GetDomainEvents());
        }

        public IEnumerable<IDomainEvent> GetAllEvents()
        {
            return _domainEvents;
        }
    }
}
