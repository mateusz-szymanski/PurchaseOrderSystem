using PurchaseOrderSystem.Domain.Common;
using PurchaseOrderSystem.Domain.FraudCheckResults.Checkers;
using PurchaseOrderSystem.Domain.Orders;
using System;

namespace PurchaseOrderSystem.Domain.FraudCheckResults
{
    public class FraudCheckResult : AggregateRoot
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }

        public bool IsFraudFree { get; private set; }
        public string FailedFraudName { get; private set; }

        private FraudCheckResult(Guid orderId, bool isFraudFree, string failedFraudName)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;

            IsFraudFree = isFraudFree;
            FailedFraudName = failedFraudName;
        }

        internal static FraudCheckResult Pass(Order order)
        {
            return new FraudCheckResult(order.Id, true, string.Empty);
        }

        internal static FraudCheckResult Fail(Order order, IFraudChecker failedFraudChecker)
        {
            return new FraudCheckResult(order.Id, false, failedFraudChecker.Name);
        }
    }
}
