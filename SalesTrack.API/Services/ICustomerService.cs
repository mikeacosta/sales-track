using SalesTrack.API.DTOs;

namespace SalesTrack.API.Services;

public interface ICustomerService
{
    Task<CustomerDto?> GetByIdAsync(int id);
    Task<IReadOnlyList<CustomerDto>> GetAllAsync();
    Task<CustomerDto> CreateAsync(CreateCustomerDto customerDto);
    Task<CustomerDto?> UpdateAsync(int id, UpdateCustomerDto customerDto);
    Task DeleteAsync(int id);
}