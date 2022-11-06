using PurchaseOrderSystem.Domain.Orders;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Domain.FraudCheckResults.Checkers
{
    public interface IFraudChecker
    {
        public string Name { get; }

        Task<bool> Check(Order order, CancellationToken cancellationToken);
    }
}