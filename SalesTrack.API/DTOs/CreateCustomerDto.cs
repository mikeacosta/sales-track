namespace SalesTrack.API.DTOs;

public class CreateCustomerDto
{
    public string Name { get; set; } = string.Empty;

    public List<CreateOrderDto> Orders { get; set; } = new();
}