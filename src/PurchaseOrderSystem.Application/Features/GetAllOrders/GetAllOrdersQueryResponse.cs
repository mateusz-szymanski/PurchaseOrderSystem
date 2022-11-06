using System.Collections.Generic;

namespace PurchaseOrderSystem.Application.Features.GetAllOrders
{
    public record GetAllOrdersQueryResponse(IEnumerable<OrderResult> Orders);
}
