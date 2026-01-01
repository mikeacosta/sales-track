namespace SalesTrack.API.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public decimal TotalAmount { get; set; }

    public int CustomerId { get; set; }
}