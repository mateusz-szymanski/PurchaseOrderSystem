using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Domain.Orders
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAll(CancellationToken cancellationToken);
        Task<decimal> GetAverageAmount(Guid orderIdToExclude, CancellationToken cancellationToken);
        Task<Order?> GetOrder(Guid orderId, CancellationToken cancellationToken);
        Task Save(Order order, CancellationToken cancellationToken);
        Task SaveMany(IEnumerable<Order> orders, CancellationToken cancellationToken);
    }
}
