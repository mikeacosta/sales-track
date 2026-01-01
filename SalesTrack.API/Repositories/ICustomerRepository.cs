using SalesTrack.API.Entities;

namespace SalesTrack.API.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int id);
    Task<IReadOnlyList<Customer>> GetAllAsync();
    Task AddAsync(Customer customer);
    void Update(Customer customer);
    void Remove(Customer customer);
    Task SaveAsync();
}