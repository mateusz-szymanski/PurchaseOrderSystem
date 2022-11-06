using System.Collections.Generic;

namespace PurchaseOrderSystem.Domain.Orders.DataPackage
{
    public record OrderData(string Email, decimal Amount, AddressData Address, IEnumerable<ProductData> Products);
}
