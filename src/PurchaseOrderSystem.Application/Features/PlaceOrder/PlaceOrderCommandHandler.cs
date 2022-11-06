using MediatR;
using Microsoft.Extensions.Logging;
using PurchaseOrderSystem.Domain.Common;
using PurchaseOrderSystem.Domain.Orders;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Application.Features.PlaceOrder
{
    public class PlaceOrderCommandHandler : AsyncRequestHandler<PlaceOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly ILogger<PlaceOrderCommandHandler> _logger;

        public PlaceOrderCommandHandler(
            IOrderRepository orderRepository,
            IDomainEventDispatcher domainEventDispatcher,
            ILogger<PlaceOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _logger = logger;
        }

        protected override async Task Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("About to create a new order");

            var order = Order.Create(request.OrderData);

            await _orderRepository.Save(order, cancellationToken);

            _logger.LogInformation("New order saved");

            _domainEventDispatcher.AddDomainEvent(order);
        }
    }
}
