using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PurchaseOrderSystem.Application.Features.InitializeOrderRepository;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.WebApi.Initialization
{
    public static class WebApplicationExtensions
    {
        public static async Task InitializeApplication(this WebApplication app)
        {
            var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
            await using var serviceScope = serviceScopeFactory.CreateAsyncScope();

            var mediator = serviceScope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Send(new InitializeOrderRepositoryCommand());
        }
    }
}
