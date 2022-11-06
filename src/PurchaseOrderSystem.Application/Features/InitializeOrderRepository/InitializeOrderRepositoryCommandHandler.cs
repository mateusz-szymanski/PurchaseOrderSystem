using MediatR;
using Microsoft.Extensions.Logging;
using PurchaseOrderSystem.Domain.Orders;
using PurchaseOrderSystem.Domain.Orders.DataPackage;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Application.Features.InitializeOrderRepository
{
    public class InitializeOrderRepositoryCommandHandler : AsyncRequestHandler<InitializeOrderRepositoryCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<InitializeOrderRepositoryCommandHandler> _logger;

        private readonly string _filePath;

        public InitializeOrderRepositoryCommandHandler(
            IOrderRepository orderRepository,
            ILogger<InitializeOrderRepositoryCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;

            var executingAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _filePath = Path.Combine(executingAssemblyLocation!, "Features", "InitializeOrderRepository", "InitialOrders.json");
        }

        protected override async Task Handle(InitializeOrderRepositoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("About to load data initial order data from file {path}", _filePath);

            var jsonContent = File.ReadAllText(_filePath);

            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            var orderData = JsonSerializer.Deserialize<IEnumerable<OrderData>>(jsonContent, jsonSerializerOptions);

            if (orderData is null)
                throw new InvalidDataException("Could not load initial orders");

            var orders = orderData.Select(od => Order.Create(od));

            _logger.LogInformation("Loading {numberOfOrders} orders", orders.Count());

            await _orderRepository.SaveMany(orders, cancellationToken);
        }
    }
}
