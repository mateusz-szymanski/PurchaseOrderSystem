using System;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Domain.FraudCheckResults
{
    public interface IFraudCheckResultRepository
    {
        Task<FraudCheckResult?> GetFraudCheckResult(Guid fraudCheckResultId, CancellationToken cancellationToken);
        Task Save(FraudCheckResult fraudCheckResult, CancellationToken cancellationToken);
    }
}
