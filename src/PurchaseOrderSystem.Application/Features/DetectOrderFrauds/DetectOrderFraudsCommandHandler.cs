using MediatR;
using Microsoft.Extensions.Logging;
using PurchaseOrderSystem.Domain.Common;
using PurchaseOrderSystem.Domain.FraudCheckResults;
using PurchaseOrderSystem.Domain.Orders;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Application.Features.DetectOrderFrauds
{
    public class DetectOrderFraudsCommandHandler : AsyncRequestHandler<DetectOrderFraudsCommand>
    {
        private readonly IAntiFraudService _antiFraudService;
        private readonly IOrderRepository _orderRepository;
        private readonly IFraudCheckResultRepository _fraudCheckResultRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly ILogger<DetectOrderFraudsCommandHandler> _logger;

        public DetectOrderFraudsCommandHandler(
            IAntiFraudService antiFraudService,
            IOrderRepository orderRepository,
            IFraudCheckResultRepository fraudCheckResultRepository,
            IDomainEventDispatcher domainEventDispatcher,
            ILogger<DetectOrderFraudsCommandHandler> logger)
        {
            _antiFraudService = antiFraudService;
            _orderRepository = orderRepository;
            _fraudCheckResultRepository = fraudCheckResultRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _logger = logger;
        }

        protected override async Task Handle(DetectOrderFraudsCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrder(request.OrderId, cancellationToken);
            if (order is null)
                throw new InvalidOperationException($"Order with id {request.OrderId} not found");

            var fraudCheckResult = await _antiFraudService.CheckForFrauds(order, cancellationToken);

            await _fraudCheckResultRepository.Save(fraudCheckResult, cancellationToken);

            _logger.LogInformation("Fraud check result for order {orderId} is saved.", order.Id);

            _domainEventDispatcher.AddDomainEvent(fraudCheckResult);
        }
    }
}
