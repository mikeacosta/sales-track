using Moq;
using SalesTrack.API.DTOs;
using SalesTrack.API.Entities;
using SalesTrack.API.Mappers;
using SalesTrack.API.Repositories;
using SalesTrack.API.Services;

namespace SalesTrack.Test;

public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _repoMock;
    private readonly IMapper _mapper;
    private readonly CustomerService _service;
    
    public CustomerServiceTests()
    {
        _repoMock = new Mock<ICustomerRepository>();
        _mapper = new Mapper();
        _service = new CustomerService(_repoMock.Object, _mapper);
    }
    
    [Fact]
    public async Task GetAllAsync_ReturnsMappedCustomersWithOrders()
    {
        // Arrange
        var customers = new List<Customer>
        {
            new Customer
            {
                Id = 1,
                Name = "Alice",
                Orders =
                {
                    new Order { Id = 10 },
                    new Order { Id = 11 }
                }
            },
            new Customer
            {
                Id = 2,
                Name = "Bob",
                Orders =
                {
                    new Order { Id = 20 }
                }
            }
        };

        _repoMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(customers);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count);

        var alice = result.First();
        Assert.Equal(1, alice.Id);
        Assert.Equal("Alice", alice.Name);
        Assert.Equal(2, alice.Orders.Count);

        var bob = result.Last();
        Assert.Equal(1, bob.Orders.Count);
    }    
    
    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenCustomerDoesNotExist()
    {
        _repoMock
            .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Customer?)null);

        var result = await _service.GetByIdAsync(99);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsMappedCustomer_WhenFound()
    {
        var customer = new Customer
        {
            Id = 1,
            Name = "Alice",
            Orders = { new Order { Id = 10 } }
        };

        _repoMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(customer);

        var result = await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Alice", result!.Name);
        Assert.Single(result.Orders);
    }
    
    [Fact]
    public async Task CreateAsync_ShouldCreateCustomerWithOrders_AndReturnDto()
    {
        // Arrange
        var orderDate = DateTimeOffset.UtcNow;

        var createDto = new CreateCustomerDto
        {
            Name = "John Doe",
            Orders =
            {
                new CreateOrderDto
                {
                    OrderDate = orderDate,
                    TotalAmount = 150m
                }
            }
        };

        Customer? savedCustomer = null;

        _repoMock
            .Setup(r => r.AddAsync(It.IsAny<Customer>()))
            .Callback<Customer>(c => savedCustomer = c)
            .Returns(Task.CompletedTask);

        _repoMock
            .Setup(r => r.SaveAsync())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.CreateAsync(createDto);

        // Assert
        Assert.NotNull(savedCustomer);
        Assert.Equal("John Doe", savedCustomer!.Name);
        Assert.Single(savedCustomer.Orders);
        Assert.Equal(150m, savedCustomer.Orders.Single().TotalAmount);

        Assert.Equal("John Doe", result.Name);
        Assert.Single(result.Orders);
        Assert.Equal(150m, result.Orders[0].TotalAmount);

        _repoMock.Verify(r => r.AddAsync(It.IsAny<Customer>()), Times.Once);
        _repoMock.Verify(r => r.SaveAsync(), Times.Once);
    }    

    [Fact]
    public async Task CreateAsync_ShouldCreateCustomerWithoutOrders()
    {
        // Arrange
        var createDto = new CreateCustomerDto
        {
            Name = "No Orders Customer"
        };

        Customer? savedCustomer = null;

        _repoMock
            .Setup(r => r.AddAsync(It.IsAny<Customer>()))
            .Callback<Customer>(c => savedCustomer = c)
            .Returns(Task.CompletedTask);

        _repoMock
            .Setup(r => r.SaveAsync())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.CreateAsync(createDto);

        // Assert
        Assert.NotNull(savedCustomer);
        Assert.Equal("No Orders Customer", savedCustomer!.Name);
        Assert.Empty(savedCustomer.Orders);

        Assert.Equal("No Orders Customer", result.Name);
        Assert.Empty(result.Orders);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateCustomerName_AndKeepOrders()
    {
        // Arrange
        var customer = new Customer
        {
            Id = 1,
            Name = "Old Name",
            Orders = new List<Order>
            {
                new Order { Id = 10, TotalAmount = 100m }
            }
        };

        _repoMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(customer);

        _repoMock
            .Setup(r => r.SaveAsync())
            .Returns(Task.CompletedTask);

        var dto = new UpdateCustomerDto
        {
            Name = "New Name"
        };

        // Act
        var result = await _service.UpdateAsync(1, dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New Name", result!.Name);

        // Orders should remain unchanged
        Assert.Single(result.Orders);
        Assert.Equal(10, result.Orders[0].Id);
        Assert.Equal(100m, result.Orders[0].TotalAmount);

        _repoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
        _repoMock.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNull_WhenCustomerNotFound()
    {
        // Arrange
        _repoMock
            .Setup(r => r.GetByIdAsync(99))
            .ReturnsAsync((Customer?)null);

        var dto = new UpdateCustomerDto { Name = "Whatever" };

        // Act
        var result = await _service.UpdateAsync(99, dto);

        // Assert
        Assert.Null(result);
        _repoMock.Verify(r => r.SaveAsync(), Times.Never);
    }
}