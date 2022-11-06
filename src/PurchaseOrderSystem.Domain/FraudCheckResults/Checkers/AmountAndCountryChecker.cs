using PurchaseOrderSystem.Domain.Orders;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Domain.FraudCheckResults.Checkers
{
    public class AmountAndCountryChecker : IFraudChecker
    {
        private const string _suspiciousCountry = "Nigeria";
        private const decimal _amountThreshold = 1000;

        public string Name => "Amount and Country";

        public Task<bool> Check(Order order, CancellationToken cancellationToken)
        {
            var isValid =
                !(
                    order.Address.Country.Equals(_suspiciousCountry) &&
                    order.Amount > _amountThreshold
                );

            return Task.FromResult(isValid);
        }
    }
}
