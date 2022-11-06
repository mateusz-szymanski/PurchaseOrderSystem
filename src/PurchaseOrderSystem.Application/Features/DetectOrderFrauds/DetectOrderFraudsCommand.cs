using MediatR;
using System;

namespace PurchaseOrderSystem.Application.Features.DetectOrderFrauds
{
    public class DetectOrderFraudsCommand : IRequest
    {
        public Guid OrderId { get; private set; }

        public DetectOrderFraudsCommand(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
