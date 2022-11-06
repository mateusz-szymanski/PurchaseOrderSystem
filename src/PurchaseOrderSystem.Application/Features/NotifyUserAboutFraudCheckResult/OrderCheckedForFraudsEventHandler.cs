using MediatR;
using PurchaseOrderSystem.Domain.FraudCheckResults.DomainEvents;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Application.Features.NotifyUserAboutFraudCheckResult
{
    internal class OrderCheckedForFraudsEventHandler : INotificationHandler<OrderCheckedForFraudsEvent>
    {
        private readonly IMediator _mediator;

        public OrderCheckedForFraudsEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(OrderCheckedForFraudsEvent notification, CancellationToken cancellationToken)
        {
            var command = new NotifyUserAboutFraudCheckResultCommand(notification.FraudCheckResultId);

            await _mediator.Send(command, cancellationToken);
        }
    }
}
