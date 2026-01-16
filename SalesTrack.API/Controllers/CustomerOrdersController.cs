using Microsoft.AspNetCore.Mvc;
using SalesTrack.API.DTOs;
using SalesTrack.API.Services;

namespace SalesTrack.API.Controllers;

[ApiController]
[Route("api/customers/{customerId:int}/orders")]
public sealed class CustomerOrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public CustomerOrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrders(int customerId)
    {
        var orders = await _orderService.GetOrdersForCustomerAsync(customerId);

        return Ok(orders);
    }
}
