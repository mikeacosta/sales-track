using SalesTrack.API.DTOs;
using SalesTrack.API.Mappers;
using SalesTrack.API.Repositories;

namespace SalesTrack.API.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    
    public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }
    
    public async Task<CustomerDto?> GetByIdAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null)
            return null;
        
        return _mapper.ToCustomerDto(customer);
    }

    public async Task<IReadOnlyList<CustomerDto>> GetAllAsync()
    {
        var customers = await _customerRepository.GetAllAsync();

        return customers
            .Select(customer => _mapper.ToCustomerDto(customer))
            .ToList();
    }

    public async Task<CustomerDto> CreateAsync(CreateCustomerDto dto)
    {
        var customer = _mapper.ToCustomerEntity(dto);

        await _customerRepository.AddAsync(customer);
        await _customerRepository.SaveAsync();

        return _mapper.ToCustomerDto(customer);
    }

    public async Task<CustomerDto?> UpdateAsync(int id, UpdateCustomerDto dto)
    {
        // Fetch the existing customer
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null)
            return null; // Not found

        // Only update customer fields (do NOT touch Orders)
        customer.Name = dto.Name;

        // Save changes
        await _customerRepository.SaveAsync();

        // Map and return
        return _mapper.ToCustomerDto(customer);
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}