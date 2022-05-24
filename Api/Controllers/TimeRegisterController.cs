using Application.Features.API.TimeRegister.Commands.ApiCreateTimeRegister;
using Application.Features.API.TimeRegister.Commands.ApiEditTimeRegistration;
using Application.Features.API.TimeRegister.Query.ApiGetRegisterById;
using Application.Features.API.TimeRegister.Query.ApiGetTimeRegistersPaginated;

namespace Api.Controllers;

[Route("Api/Customers/{customerId:guid}/Projects/{projectId:guid}/[controller]")]
[ApiController]
public class TimeRegisterController : ControllerBase
{
    private readonly IMediator _mediator;

    public TimeRegisterController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllForCustomer(Guid customerId, Guid projectId, int limit = 20, int offset = 0)
    {
        var response = await _mediator.Send(new ApiGetTimeRegistersForProjectPaginatedQuery(customerId,
            projectId, limit, offset));


        if (response.StatusCode == IResponse.Status.Success)
        {
            return Ok(new {Data = response.TimeRegistrations, response.TotalCount, response.StatusText});
        }

        if (response.StatusCode == IResponse.Status.NotFound)
        {
            return NotFound(new {response.StatusText});
        }


        return StatusCode(StatusCodes.Status500InternalServerError, new {response.StatusText});
    }

    [HttpGet("{timeRegistrationId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid customerId, Guid projectId, Guid timeRegistrationId)
    {
        var response = await _mediator.Send(new ApiGetTimeRegistrationByIdQuery(customerId,
            projectId, timeRegistrationId));

        if (response.StatusCode == IResponse.Status.Success)
        {
            return Ok(new {response.TimeRegistrations});
        }

        return response.StatusCode == IResponse.Status.NotFound
            ? NotFound(new {response.StatusText})
            : StatusCode(StatusCodes.Status500InternalServerError, new {response.StatusText});
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(Guid customerId, Guid projectId,
         ApiCreateTimeRegisterCommand.CreateTimeRegisterModel model)
    {
        var response = await _mediator.Send(new ApiCreateTimeRegisterCommand(customerId, projectId, model));

        if (response.StatusCode == IResponse.Status.Success)
        {
            return CreatedAtAction(nameof(GetById),
                new
                {
                    CustomerId = customerId,
                    ProjectId = projectId,
                    TimeRegistrationId = response.TimeRegistration!.Id
                },
                response.TimeRegistration);
        }

        if (response.StatusCode == IResponse.Status.NotFound)
        {
            return NotFound(new {response.StatusText});
        }


        if (response.Errors is null || response.Errors.Count < 1)
        {
            return BadRequest(new {response.StatusText});
        }


        var errors = new List<string>();
        response.Errors.ForEach(x => errors.Add(x.ErrorMessage));
        return BadRequest(new {response.StatusText, Errors = errors});
    }


    [HttpPut("{timeRegistrationId:guid}")]
    [Authorize("admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Edit(Guid customerId, Guid projectId, Guid timeRegistrationId,
        [FromBody] ApiEditTimeRegistrationCommand.EditTimeRegistrationModel model)
    {
        var response =
            await _mediator.Send(new ApiEditTimeRegistrationCommand(customerId, projectId, timeRegistrationId, model));

        if (response.StatusCode == IResponse.Status.Success)
        {
            return Ok(new {response.TimeRegistration, response.StatusText});
        }


        return response.StatusCode == IResponse.Status.NotFound
            ? NoContent()
            : StatusCode(StatusCodes.Status500InternalServerError, new {response.StatusText});
    }
}
