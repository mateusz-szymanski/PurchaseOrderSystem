using Microsoft.Extensions.DependencyInjection;
using PurchaseOrderSystem.Application.Services;
using PurchaseOrderSystem.Domain.FraudCheckResults;
using PurchaseOrderSystem.Domain.Orders;
using PurchaseOrderSystem.Infrastructure.MessageQueueSystem;
using PurchaseOrderSystem.Infrastructure.Repositories;

namespace PurchaseOrderSystem.Infrastructure.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IMessageQueue, MessageQueue>();

            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton<IFraudCheckResultRepository, FraudCheckResultRepository>();

            return services;
        }
    }
}
