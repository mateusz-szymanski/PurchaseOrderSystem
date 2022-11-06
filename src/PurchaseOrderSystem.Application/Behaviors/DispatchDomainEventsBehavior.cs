using MediatR;
using Microsoft.Extensions.Logging;
using PurchaseOrderSystem.Application.Services;
using PurchaseOrderSystem.Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Application.Behaviors
{
    internal class DispatchDomainEventsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IMessageQueue _messageQueue;
        private readonly ILogger<DispatchDomainEventsBehavior<TRequest, TResponse>> _logger;

        public DispatchDomainEventsBehavior(
            IDomainEventDispatcher domainEventDispatcher,
            IMessageQueue messageQueue,
            ILogger<DispatchDomainEventsBehavior<TRequest, TResponse>> logger)
        {
            _domainEventDispatcher = domainEventDispatcher;
            _messageQueue = messageQueue;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();

            var domainEventsToEnqueue = _domainEventDispatcher.GetAllEvents();
            foreach (var domainEvent in domainEventsToEnqueue)
            {
                _logger.LogDebug("Raising domain event: {@domainEvent}", domainEvent);
                await _messageQueue.Enqueue(domainEvent, CancellationToken.None);
            }

            return response;
        }
    }
}
