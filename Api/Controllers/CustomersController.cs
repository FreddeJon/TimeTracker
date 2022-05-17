using Application.Contracts.Responses;
using Application.Features.API.Customers.Query.ApiGetCustomers;
using Application.Features.Customer.Command.CreateCustomer;
using Application.Features.Customer.Query.GetCustomerById;
using Application.Features.Customer.Query.GetCustomersWithProjectsPaginated;
using MediatR;

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
        var response = await _mediator.Send(new GetCustomerByIdQuery(customerId));

        if (response.StatusCode == IResponse.Status.Success)
        {
            return Ok(response.Customer);
        }

        return NotFound();
    }



    public class CreateCustomerModel
    {
        public string Name { get; set; }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCustomerModel model)
    {
        var response = await _mediator.Send(new CreateCustomerCommand(model.Name));

        if (response.StatusCode == IResponse.Status.Success)
        {
            return CreatedAtAction(nameof(GetById), new { customerId = response.Customer.Id }, response.Customer);
        }

        var errors = new List<string>();

        response.Errors.ForEach(x => errors.Add(x.ErrorMessage));

        return BadRequest(new { Status = response.StatusText, Errors = errors });
    }


    [HttpPut("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Put(Guid customerId)
    {
        return Ok("Hey");
    }

}


