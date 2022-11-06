using MediatR;
using Microsoft.Extensions.Logging;
using PurchaseOrderSystem.Domain.Common;
using PurchaseOrderSystem.Domain.FraudCheckResults;
using PurchaseOrderSystem.Domain.Orders;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Application.Features.NotifyUserAboutFraudCheckResult
{
    public class NotifyUserAboutFraudCheckResultCommandHandler : AsyncRequestHandler<NotifyUserAboutFraudCheckResultCommand>
    {
        private readonly IFraudCheckResultRepository _fraudCheckResultRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly ILogger<NotifyUserAboutFraudCheckResultCommandHandler> _logger;

        public NotifyUserAboutFraudCheckResultCommandHandler(
            IFraudCheckResultRepository fraudCheckResultRepository,
            IOrderRepository orderRepository,
            IDomainEventDispatcher domainEventDispatcher,
            ILogger<NotifyUserAboutFraudCheckResultCommandHandler> logger)
        {
            _fraudCheckResultRepository = fraudCheckResultRepository;
            _orderRepository = orderRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _logger = logger;
        }

        protected override async Task Handle(NotifyUserAboutFraudCheckResultCommand request, CancellationToken cancellationToken)
        {
            var fraudCheckResult = await _fraudCheckResultRepository.GetFraudCheckResult(request.FraudCheckResultId, cancellationToken);
            if (fraudCheckResult is null)
                throw new InvalidOperationException($"Fraud check result with id {request.FraudCheckResultId} not found");

            var order = await _orderRepository.GetOrder(fraudCheckResult.OrderId, cancellationToken);
            if (order is null)
                throw new InvalidOperationException($"Order with id {fraudCheckResult.OrderId} not found");

            // TODO: notify user signalR, email, etc.
            if (fraudCheckResult.IsFraudFree)
            {
                _logger.LogInformation("User {email} notified that order with amount {amount} is fraud free.", order.Email, order.Amount);
            }
            else
            {
                _logger.LogInformation("User {email} notified that order with amount {amount} is suspened due to fraud detected.", order.Email, order.Amount);
            }

            _domainEventDispatcher.AddDomainEvent(fraudCheckResult);
        }
    }
}
