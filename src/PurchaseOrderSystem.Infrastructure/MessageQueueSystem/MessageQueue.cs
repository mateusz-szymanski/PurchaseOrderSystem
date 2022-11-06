using PurchaseOrderSystem.Application.Services;
using PurchaseOrderSystem.Domain.Common;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Infrastructure.MessageQueueSystem
{
    internal class MessageQueue : IMessageQueue
    {
        private readonly Channel<IDomainEvent> _channel;

        public MessageQueue()
        {
            _channel = Channel.CreateUnbounded<IDomainEvent>();
        }

        public async Task Enqueue(IDomainEvent message, CancellationToken cancellationToken)
        {
            await _channel.Writer.WriteAsync(message, cancellationToken);
        }

        public async Task<IDomainEvent> Dequeue(CancellationToken cancellationToken)
        {
            return await _channel.Reader.ReadAsync(cancellationToken);
        }
    }
}
