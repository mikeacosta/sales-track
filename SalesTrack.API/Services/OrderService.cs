using SalesTrack.API.DTOs;
using SalesTrack.API.Mappers;
using SalesTrack.API.Repositories;

namespace SalesTrack.API.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    
    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    
    public async Task<IReadOnlyList<OrderDto>> GetOrdersForCustomerAsync(int customerId)
    {
        var orders = await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
        
        return orders
            .Select(order => _mapper.ToOrderDto(order))
            .ToList();
    }
}