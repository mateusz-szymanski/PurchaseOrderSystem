using PurchaseOrderSystem.Domain.FraudCheckResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Infrastructure.Repositories
{
    internal class FraudCheckResultRepository : IFraudCheckResultRepository
    {
        private readonly List<FraudCheckResult> _fraudCheckResults = new();

        public Task<FraudCheckResult?> GetFraudCheckResult(Guid fraudCheckResultId, CancellationToken cancellationToken)
        {
            var result = (from fraudCheckResult in _fraudCheckResults
                          where fraudCheckResultId == fraudCheckResult.Id
                          select fraudCheckResult
                         )
                         .SingleOrDefault();

            return Task.FromResult(result);
        }

        public Task Save(FraudCheckResult fraudCheckResult, CancellationToken cancellationToken)
        {
            _fraudCheckResults.Add(fraudCheckResult);

            return Task.CompletedTask;
        }
    }
}
