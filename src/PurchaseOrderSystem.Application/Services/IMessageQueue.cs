using PurchaseOrderSystem.Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Application.Services
{
    public interface IMessageQueue
    {
        Task<IDomainEvent> Dequeue(CancellationToken cancellationToken);
        Task Enqueue(IDomainEvent message, CancellationToken cancellationToken);
    }
}