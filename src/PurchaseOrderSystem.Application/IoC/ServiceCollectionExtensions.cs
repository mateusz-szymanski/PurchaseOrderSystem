using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PurchaseOrderSystem.Application.Behaviors;

namespace PurchaseOrderSystem.Application.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DispatchDomainEventsBehavior<,>));

            return services;
        }
    }
}
