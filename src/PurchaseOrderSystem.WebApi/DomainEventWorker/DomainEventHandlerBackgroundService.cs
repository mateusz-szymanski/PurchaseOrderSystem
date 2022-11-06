using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PurchaseOrderSystem.Application.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.WebApi.DomainEventWorker
{
    public class DomainEventHandlerBackgroundService : BackgroundService
    {
        private readonly IMessageQueue _messageQueue;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<DomainEventHandlerBackgroundService> _logger;

        public DomainEventHandlerBackgroundService(
            IMessageQueue messageQueue,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<DomainEventHandlerBackgroundService> logger)
        {
            _messageQueue = messageQueue;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting domain event handler background service...");

            while (!stoppingToken.IsCancellationRequested)
            {
                var domainEvent = await _messageQueue.Dequeue(stoppingToken);

                await using var serviceScope = _serviceScopeFactory.CreateAsyncScope();
                var mediator = serviceScope.ServiceProvider.GetRequiredService<IMediator>();

                try
                {
                    _logger.LogInformation("About to publish domain event {@domainEvent}.", domainEvent);

                    await mediator.Publish(domainEvent, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to handle domain event {@domainEvent}.", domainEvent);

                    continue;
                }
            }
        }
    }
}
