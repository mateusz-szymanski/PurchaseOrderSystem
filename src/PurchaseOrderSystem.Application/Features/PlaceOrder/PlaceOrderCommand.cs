using MediatR;
using PurchaseOrderSystem.Domain.Orders.DataPackage;

namespace PurchaseOrderSystem.Application.Features.PlaceOrder
{
    public class PlaceOrderCommand : IRequest
    {
        public OrderData OrderData { get; private set; }

        public PlaceOrderCommand(OrderData orderData)
        {
            OrderData = orderData;
        }
    }
}
