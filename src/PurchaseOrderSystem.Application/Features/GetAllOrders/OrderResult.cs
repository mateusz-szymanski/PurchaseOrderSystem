using System.Collections.Generic;

namespace PurchaseOrderSystem.Application.Features.GetAllOrders
{
    public record AddressResult(string Street, string ZipCode, string City, string Country);
    public record ProductResult(string Name, int Quantity);
    public record OrderResult(string Email, decimal Amount, AddressResult Address, IEnumerable<ProductResult> Products);
}
