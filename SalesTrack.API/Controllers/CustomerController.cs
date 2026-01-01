using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SalesTrack.API.DTOs;
using SalesTrack.API.Services;

namespace SalesTrack.API.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }
    
    /// <summary>
    /// Gets all customers including their orders.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CustomerDto>>> GetAll()
    {
        var customers = await _customerService.GetAllAsync();
        return Ok(customers);
    }
    
    /// <summary>
    /// Gets a single customer by id including their orders.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDto>> GetById(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);

        if (customer == null)
            return NotFound();

        return Ok(customer);
    }
    
    /// <summary>
    /// Creates a new customer with orders.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerDto>> Create([FromBody] CreateCustomerDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdCustomer = await _customerService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdCustomer.Id },
            createdCustomer);
    }

    /// <summary>
    /// Update a customer.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerDto>> Update(int id, [FromBody] UpdateCustomerDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedCustomer = await _customerService.UpdateAsync(id, dto);

        if (updatedCustomer == null)
            return NotFound();

        return Ok(updatedCustomer);
    }
    
}