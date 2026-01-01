using SalesTrack.API.DTOs;
using SalesTrack.API.Entities;

namespace SalesTrack.API.Mappers;

public class Mapper : IMapper
{
    public CustomerDto ToCustomerDto(Customer customer)
    {
        var dto = new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name
        };
        
        foreach (var order in customer.Orders)
            dto.Orders.Add(ToOrderDto(order));
        
        return dto;
    }

    public OrderDto ToOrderDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            CustomerId = order.CustomerId
        };
    }

    public Customer ToCustomerEntity(CreateCustomerDto createCustomerDto)
    {
        var customer = new Customer
        {
            Name = createCustomerDto.Name
        };
        
        foreach (var orderDto in createCustomerDto.Orders)
            customer.Orders.Add(ToOrderEntity(orderDto));
        
        return customer;
    }

    public Order ToOrderEntity(CreateOrderDto createOrderDto)
    {
        return new Order
        {
            OrderDate = createOrderDto.OrderDate,
            TotalAmount = createOrderDto.TotalAmount,
            CustomerId = createOrderDto.CustomerId
        };
    }
}