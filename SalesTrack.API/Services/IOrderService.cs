using SalesTrack.API.DTOs;

namespace SalesTrack.API.Services;

public interface IOrderService
{
    Task<IReadOnlyList<OrderDto>> GetOrdersForCustomerAsync(int customerId);
    Task<OrderDto?> GetOrderForCustomerAsync(int customerId, int orderId);
}