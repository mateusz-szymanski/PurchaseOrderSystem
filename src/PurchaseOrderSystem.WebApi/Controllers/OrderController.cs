using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurchaseOrderSystem.Application.Features.GetAllOrders;
using PurchaseOrderSystem.Application.Features.PlaceOrder;
using PurchaseOrderSystem.Domain.Orders.DataPackage;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllOrdersQuery(), cancellationToken);

            return Ok(response.Orders);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderData order, CancellationToken cancellationToken)
        {
            await _mediator.Send(new PlaceOrderCommand(order), cancellationToken);

            return NoContent();
        }
    }
}