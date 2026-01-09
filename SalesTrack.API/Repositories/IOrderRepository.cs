using SalesTrack.API.Entities;

namespace SalesTrack.API.Repositories;

public interface IOrderRepository
{
    Task<IReadOnlyList<Order>> GetOrdersByCustomerIdAsync(int customerId);
}