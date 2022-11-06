using PurchaseOrderSystem.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Infrastructure.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new();

        public Task<IEnumerable<Order>> GetAll(CancellationToken cancellationToken)
        {
            var orders = (from order in _orders
                          orderby order.PlacedAt descending
                          select order
                          )
                          .ToList();

            return Task.FromResult(orders.AsEnumerable());
        }

        public Task<decimal> GetAverageAmount(Guid orderIdToExclude, CancellationToken cancellationToken)
        {
            var averageAmount = (from order in _orders
                                 where order.Id != orderIdToExclude
                                 select order.Amount
                                 )
                                 .Average();

            return Task.FromResult(averageAmount);
        }

        public Task<Order?> GetOrder(Guid orderId, CancellationToken cancellationToken)
        {
            var result = (from order in _orders
                          where orderId == order.Id
                          select order
                         )
                         .SingleOrDefault();

            return Task.FromResult(result);
        }

        public Task Save(Order order, CancellationToken cancellationToken)
        {
            _orders.Add(order);

            return Task.CompletedTask;
        }

        public Task SaveMany(IEnumerable<Order> orders, CancellationToken cancellationToken)
        {
            _orders.AddRange(orders);

            return Task.CompletedTask;
        }
    }
}
