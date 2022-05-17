﻿using Application.Contracts.Responses;
using Application.Features.API.Customers.Commands.ApiCreateCustomer;
using Application.Features.API.Customers.Commands.ApiEditCustomer;
using Application.Features.API.Customers.Query.ApiGetCustomerById;
using Application.Features.API.Customers.Query.ApiGetCustomers;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMapper mapper, IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(int limit = 20, int offset = 0)
    {
        var response = await _mediator.Send(new ApiGetCustomersPaginatedQuery(limit, offset));

        return response.StatusCode == IResponse.Status.Error ? StatusCode(StatusCodes.Status500InternalServerError, response.StatusText) :
            Ok(new { response.StatusText, response.TotalCount, Data = response.Customers });
    }



    [HttpGet("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid customerId)
    {
        var response = await _mediator.Send(new ApiGetCustomerByIdQuery(customerId));

        return response.StatusCode == IResponse.Status.Success ? Ok(response.Customer) : NotFound();
    }



    public class CreateCustomerModel
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCustomerModel model)
    {
        var response = await _mediator.Send(new ApiCreateCustomerCommand(model.Name));

        if (response.StatusCode == IResponse.Status.Success)
        {
            return CreatedAtAction(nameof(GetById), new { customerId = response.Customer!.Id }, response.Customer);
        }


        if (response.Errors is null || response.Errors.Count < 1)
        {
            return BadRequest(new { Status = response.StatusText });
        }


        var errors = new List<string>();
        response.Errors.ForEach(x => errors.Add(x.ErrorMessage));
        return BadRequest(new {response.StatusText, Errors = errors });
    }



    public class EditCustomerModel
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
    }
    [HttpPut("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(Guid customerId, [FromBody] EditCustomerModel model)
    {
        var response = await _mediator.Send(new ApiEditCustomerCommand(customerId,model.Name));

        if (response.StatusCode == IResponse.Status.Success) return Ok(response.Customer);

        if (response.StatusCode == IResponse.Status.NotFound) return NotFound(response.StatusText);
    


        if (response.Errors is null || response.Errors.Count < 1)
        {
            return BadRequest(new { Status = response.StatusText });
        }


        var errors = new List<string>();
        response.Errors.ForEach(x => errors.Add(x.ErrorMessage));
        return BadRequest(new { response.StatusText, Errors = errors });
    }
}


