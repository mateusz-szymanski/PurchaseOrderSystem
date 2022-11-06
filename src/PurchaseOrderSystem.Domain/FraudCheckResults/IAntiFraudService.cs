using PurchaseOrderSystem.Domain.Orders;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Domain.FraudCheckResults
{
    public interface IAntiFraudService
    {
        Task<FraudCheckResult> CheckForFrauds(Order order, CancellationToken cancellationToken);
    }
}