using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesTrack.API.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }    
    
    public DateTimeOffset OrderDate { get; set; }
    
    [Precision(18, 2)]
    public decimal TotalAmount { get; set; }
    
    [ForeignKey("Customer")]
    public int CustomerId { get; set; }
    
    public Customer Customer { get; set; } = null!;
}