using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SalesTrack.API.Entities;

namespace SalesTrack.API.Data;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var converter = new ValueConverter<DateTimeOffset, DateTimeOffset>(
            v => v.ToUniversalTime(),
            v => v
        );

        modelBuilder.Entity<Order>()
            .Property(e => e.OrderDate)
            .HasConversion(converter);
    }    
}