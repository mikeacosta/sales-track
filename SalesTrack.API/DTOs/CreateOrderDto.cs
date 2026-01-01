namespace SalesTrack.API.DTOs;

public class CreateOrderDto
{
    public DateTimeOffset OrderDate { get; set; }
    public decimal TotalAmount { get; set; }

    public int CustomerId { get; set; }
}