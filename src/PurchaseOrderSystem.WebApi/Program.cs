using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PurchaseOrderSystem.Application.IoC;
using PurchaseOrderSystem.Domain.IoC;
using PurchaseOrderSystem.Infrastructure.IoC;
using PurchaseOrderSystem.WebApi.DomainEventWorker;
using PurchaseOrderSystem.WebApi.Initialization;
using PurchaseOrderSystem.WebApi.IoC;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.WebApi
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHostedService<DomainEventHandlerBackgroundService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDomainServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices();

            builder.Services.AddCustomProblemDetails();

            var app = builder.Build();

            await app.InitializeApplication();

            app.UseProblemDetails();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}