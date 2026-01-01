using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesTrack.API.Entities;

public class Customer
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(100)]
    public required string Name { get; set; }
    
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}