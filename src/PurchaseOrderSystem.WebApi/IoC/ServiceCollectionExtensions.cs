using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PurchaseOrderSystem.WebApi.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services)
        {
            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (_, _) => false;

                options.Map<ArgumentException>(ex => new ProblemDetails()
                {
                    Status = 400,
                    Detail = ex.Message,
                    Title = "Bad Request",
                    Type = "https://httpstatuses.io/400"
                });
            });

            return services;
        }
    }
}
