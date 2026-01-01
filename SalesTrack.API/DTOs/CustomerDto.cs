namespace SalesTrack.API.DTOs;

public class CustomerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<OrderDto> Orders { get; set; } = new();
}