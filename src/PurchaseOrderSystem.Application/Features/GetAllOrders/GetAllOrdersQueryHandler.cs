using MediatR;
using Microsoft.Extensions.Logging;
using PurchaseOrderSystem.Domain.Orders;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Application.Features.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, GetAllOrdersQueryResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<GetAllOrdersQueryHandler> _logger;

        public GetAllOrdersQueryHandler(
            IOrderRepository orderRepository,
            ILogger<GetAllOrdersQueryHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<GetAllOrdersQueryResponse> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all orders...");

            var orders = await _orderRepository.GetAll(cancellationToken);

            var results = orders.Select(o =>
                new OrderResult(
                    Email: o.Email,
                    Amount: o.Amount,
                    Address: new AddressResult(
                        City: o.Address.City,
                        Country: o.Address.Country,
                        Street: o.Address.Street,
                        ZipCode: o.Address.ZipCode
                    ),
                    Products: o.Products
                        .Select(p =>
                            new ProductResult(
                                Name: p.Name,
                                Quantity: p.Quantity
                        )
                    )
                )
            );

            return new GetAllOrdersQueryResponse(results);
        }
    }
}
