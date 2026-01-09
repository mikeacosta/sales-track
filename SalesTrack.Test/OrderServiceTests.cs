using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using SalesTrack.API.Entities;
using SalesTrack.API.Mappers;
using SalesTrack.API.Repositories;
using SalesTrack.API.Services;
using Xunit;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly IMapper _mapper;
    private readonly OrderService _service;

    public OrderServiceTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _mapper = new Mapper();
        _service = new OrderService( _orderRepositoryMock.Object, _mapper);
    }

    [Fact]
    public async Task GetOrdersForCustomerAsync_ReturnsMappedOrderDtos()
    {
        // Arrange
        var customerId = 5;

        var orders = new List<Order>
        {
            new Order
            {
                Id = 1,
                CustomerId = customerId,
                OrderDate = DateTimeOffset.UtcNow.AddDays(-1),
                TotalAmount = 100m
            },
            new Order
            {
                Id = 2,
                CustomerId = customerId,
                OrderDate = DateTimeOffset.UtcNow,
                TotalAmount = 50m
            }
        };

        _orderRepositoryMock
            .Setup(r => r.GetOrdersByCustomerIdAsync(customerId))
            .ReturnsAsync(orders);

        // Act
        var result = await _service.GetOrdersForCustomerAsync(customerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        var first = result.First(o => o.Id == 1);
        Assert.Equal(1, first.Id);
        Assert.Equal(customerId, first.CustomerId);
        Assert.Equal(100m, first.TotalAmount);

        var second = result.First(o => o.Id == 2);
        Assert.Equal(2, second.Id);
        Assert.Equal(customerId, second.CustomerId);
        Assert.Equal(50m, second.TotalAmount);

        _orderRepositoryMock.Verify(
            r => r.GetOrdersByCustomerIdAsync(customerId),
            Times.Once);
    }

    [Fact]
    public async Task GetOrdersForCustomerAsync_ReturnsEmptyList_WhenNoOrdersExist()
    {
        // Arrange
        var customerId = 99;

        _orderRepositoryMock
            .Setup(r => r.GetOrdersByCustomerIdAsync(customerId))
            .ReturnsAsync(new List<Order>());

        // Act
        var result = await _service.GetOrdersForCustomerAsync(customerId);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);

        _orderRepositoryMock.Verify(
            r => r.GetOrdersByCustomerIdAsync(customerId),
            Times.Once);
    }
}
