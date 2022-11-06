using MediatR;
using Microsoft.Extensions.Logging;
using PurchaseOrderSystem.Domain.Orders.DomainEvents;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Application.Features.DetectOrderFrauds
{
    internal class OrderPlacedEventHandler : INotificationHandler<OrderPlacedEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderPlacedEventHandler> _logger;

        private readonly TimeSpan _fixedDelayBeforeFraudChecking = TimeSpan.FromSeconds(5);

        public OrderPlacedEventHandler(IMediator mediator, ILogger<OrderPlacedEventHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
        {
            var command = new DetectOrderFraudsCommand(notification.OrderId);

            _logger.LogInformation("Waiting {_fixedDelayBeforeFraudChecking} before checking for fraud.", _fixedDelayBeforeFraudChecking);

            await Task.Delay(_fixedDelayBeforeFraudChecking, cancellationToken);

            await _mediator.Send(command, cancellationToken);
        }
    }
}
