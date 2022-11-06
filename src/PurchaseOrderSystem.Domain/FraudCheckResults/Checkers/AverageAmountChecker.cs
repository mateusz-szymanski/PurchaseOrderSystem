using Microsoft.Extensions.Logging;
using PurchaseOrderSystem.Domain.Orders;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Domain.FraudCheckResults.Checkers
{
    public class AverageAmountChecker : IFraudChecker
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<AverageAmountChecker> _logger;

        public string Name => "Average amount";

        public AverageAmountChecker(IOrderRepository orderRepository, ILogger<AverageAmountChecker> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<bool> Check(Order order, CancellationToken cancellationToken)
        {
            var lastOrdersAverageAmount = await _orderRepository.GetAverageAmount(order.Id, cancellationToken);

            _logger.LogDebug("Average from last orders: {averageOfLastOrders}.", lastOrdersAverageAmount);

            var isValid = order.Amount <= lastOrdersAverageAmount * 5;

            return isValid;
        }
    }
}
