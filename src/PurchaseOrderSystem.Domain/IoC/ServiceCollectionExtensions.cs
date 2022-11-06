using Microsoft.Extensions.DependencyInjection;
using PurchaseOrderSystem.Domain.Common;
using PurchaseOrderSystem.Domain.FraudCheckResults;
using PurchaseOrderSystem.Domain.FraudCheckResults.Checkers;

namespace PurchaseOrderSystem.Domain.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IAntiFraudService, AntiFraudService>();
            services.AddScoped<IFraudChecker, AmountAndCountryChecker>();
            services.AddScoped<IFraudChecker, AverageAmountChecker>();

            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            return services;
        }
    }
}
