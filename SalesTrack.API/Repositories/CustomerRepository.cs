using Microsoft.EntityFrameworkCore;
using SalesTrack.API.Data;
using SalesTrack.API.Entities;

namespace SalesTrack.API.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;
    
    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _context.Customers
            .Include(c => c.Orders) // eager load orders
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IReadOnlyList<Customer>> GetAllAsync()
    {
        return await _context.Customers
            .Include(c => c.Orders)
            .ToListAsync();
    }

    public async Task AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
    }

    public void Update(Customer customer)
    {
        _context.Customers.Update(customer);
    }

    public void Remove(Customer customer)
    {
        _context.Customers.Remove(customer);
    }
    
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }   
}