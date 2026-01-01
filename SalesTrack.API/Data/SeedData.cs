using Microsoft.EntityFrameworkCore;
using SalesTrack.API.Entities;

namespace SalesTrack.API.Data;

public static class SeedData
{
    public static void SeedDatabase(AppDbContext context)
    {
        context.Database.Migrate();
        
        if (context.Orders.Any())
            return;
        
        var customers = new List<Customer>
        {
            new Customer
            {
                Name = "Aisha Mohammed",
                Orders = new List<Order>
                {
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-10), TotalAmount = 150.25m },
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-5), TotalAmount = 75.50m }
                }
            },
            new Customer
            {
                Name = "Hiroshi Tanaka",
                Orders = new List<Order>
                {
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-7), TotalAmount = 200.00m },
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-2), TotalAmount = 50.75m }
                }
            },
            new Customer
            {
                Name = "Carlos Ramirez",
                Orders = new List<Order>
                {
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-3), TotalAmount = 120.00m }
                }
            },
            new Customer
            {
                Name = "Priya Patel",
                Orders = new List<Order>
                {
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-8), TotalAmount = 300.00m },
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-4), TotalAmount = 45.50m },
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-1), TotalAmount = 89.99m }
                }
            },
            new Customer
            {
                Name = "Kwame Nkrumah",
                Orders = new List<Order>
                {
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-6), TotalAmount = 250.00m },
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-3), TotalAmount = 150.75m }
                }
            },
            new Customer
            {
                Name = "Mei-Ling Chen",
                Orders = new List<Order>
                {
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-12), TotalAmount = 99.99m },
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-7), TotalAmount = 60.00m },
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-2), TotalAmount = 45.25m }
                }
            },
                new Customer
            {
                Name = "Liam O'Connor", // Irish
                Orders = new List<Order>
                {
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-15), TotalAmount = 180.50m },
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-7), TotalAmount = 90.00m }
                }
            },
            new Customer
            {
                Name = "Fatima Al-Farsi", // Middle Eastern
                Orders = new List<Order>
                {
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-20), TotalAmount = 250.75m },
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-10), TotalAmount = 120.00m }
                }
            },
            new Customer
            {
                Name = "Jun Ho Kim", // Korean
                Orders = new List<Order>
                {
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-8), TotalAmount = 300.00m }
                }
            },
            new Customer
            {
                Name = "Ana Souza", // Brazilian
                Orders = new List<Order>
                {
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-12), TotalAmount = 75.25m },
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-3), TotalAmount = 150.00m }
                }
            },
            new Customer
            {
                Name = "Rajesh Singh", // Indian
                Orders = new List<Order>
                {
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-5), TotalAmount = 220.00m },
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-1), TotalAmount = 85.75m }
                }
            },
            new Customer
            {
                Name = "Chimamanda Okafor", // Nigerian
                Orders = new List<Order>
                {
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-18), TotalAmount = 135.00m },
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-7), TotalAmount = 60.50m }
                }
            },
            new Customer
            {
                Name = "Sofia Rossi", // Italian
                Orders = new List<Order>
                {
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-14), TotalAmount = 190.00m },
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-2), TotalAmount = 75.25m }
                }
            },
            new Customer
            {
                Name = "Yuki Nakamura", // Japanese
                Orders = new List<Order>
                {
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-9), TotalAmount = 250.00m }
                }
            },
            new Customer
            {
                Name = "Diego Martínez", // Spanish/Latin American
                Orders = new List<Order>
                {
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-6), TotalAmount = 180.00m },
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-3), TotalAmount = 95.50m },
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-1), TotalAmount = 60.00m }
                }
            },
            new Customer
            {
                Name = "Amina Khan", // Pakistani
                Orders = new List<Order>
                {
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-11), TotalAmount = 120.00m },
                    new Order { OrderDate = DateTimeOffset.Now.AddDays(-4), TotalAmount = 85.00m }
                }
            }
        };

        context.Customers.AddRange(customers);
        context.SaveChanges();
    }
}