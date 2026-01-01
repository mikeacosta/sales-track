using SalesTrack.API.DTOs;
using SalesTrack.API.Entities;

namespace SalesTrack.API.Mappers;

public interface IMapper
{
    CustomerDto ToCustomerDto(Customer customer);
    OrderDto ToOrderDto(Order order);
    Customer ToCustomerEntity(CreateCustomerDto createCustomerDto);
    Order ToOrderEntity(CreateOrderDto createOrderDto);
}