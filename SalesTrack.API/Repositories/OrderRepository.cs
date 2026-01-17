using Microsoft.EntityFrameworkCore;
using SalesTrack.API.Data;
using SalesTrack.API.Entities;

namespace SalesTrack.API.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Order>> GetOrdersByCustomerIdAsync(int customerId)
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(o => o.CustomerId == customerId)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }
    
    public async Task<Order?> GetOrderForCustomerAsync(int customerId, int orderId)
    {
        return await _context.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.CustomerId == customerId && o.Id == orderId);
    }   
}