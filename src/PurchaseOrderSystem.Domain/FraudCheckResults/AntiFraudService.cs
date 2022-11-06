using Microsoft.Extensions.Logging;
using PurchaseOrderSystem.Domain.Common;
using PurchaseOrderSystem.Domain.FraudCheckResults.Checkers;
using PurchaseOrderSystem.Domain.FraudCheckResults.DomainEvents;
using PurchaseOrderSystem.Domain.Orders;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Domain.FraudCheckResults
{
    public class AntiFraudService : IAntiFraudService
    {
        private readonly IEnumerable<IFraudChecker> _fraudCheckers;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly ILogger<AntiFraudService> _logger;

        public AntiFraudService(
            IEnumerable<IFraudChecker> fraudCheckers,
            IDomainEventDispatcher domainEventDispatcher,
            ILogger<AntiFraudService> logger)
        {
            _fraudCheckers = fraudCheckers;
            _domainEventDispatcher = domainEventDispatcher;
            _logger = logger;
        }

        public async Task<FraudCheckResult> CheckForFrauds(Order order, CancellationToken cancellationToken)
        {
            var fraudCheckResult = FraudCheckResult.Pass(order);

            _logger.LogInformation("Checking order {orderId} for frauds.", order.Id);

            foreach (var fraudChecker in _fraudCheckers)
            {
                var isFraudFree = await fraudChecker.Check(order, cancellationToken);

                _logger.LogDebug("Is fraud {fraudChecker} free: {isFraudFree}.", fraudChecker.Name, isFraudFree);

                if (!isFraudFree)
                {
                    fraudCheckResult = FraudCheckResult.Fail(order, fraudChecker);
                    break;
                }
            }

            _logger.LogInformation("Checking order {orderId} for frauds result: {@fraudCheckResult}.", order.Id, fraudCheckResult);

            _domainEventDispatcher.AddDomainEvent(new OrderCheckedForFraudsEvent(fraudCheckResult));

            return fraudCheckResult;
        }
    }
}
