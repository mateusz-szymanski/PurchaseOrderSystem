using PurchaseOrderSystem.Domain.Common;
using System;

namespace PurchaseOrderSystem.Domain.FraudCheckResults.DomainEvents
{
    public class OrderCheckedForFraudsEvent : IDomainEvent
    {
        public Guid FraudCheckResultId { get; private set; }

        public OrderCheckedForFraudsEvent(FraudCheckResult fraudCheckResult)
        {
            FraudCheckResultId = fraudCheckResult.Id;
        }
    }
}
