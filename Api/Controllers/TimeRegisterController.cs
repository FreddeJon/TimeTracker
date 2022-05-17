

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Application.Contracts.Responses;
using Application.Features.API.TimeRegister.Commands.ApiCreateTimeRegister;
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
        var response = await _mediator.Send(new ApiGetTimeRegistersForProjectPaginatedQuery(customerId: customerId,
            projectId: projectId, limit: limit, offset: offset));


        if (response.StatusCode == IResponse.Status.Success)
            return Ok(new { Data = response.TimeRegistrations, response.TotalCount, response.StatusText });

        if (response.StatusCode == IResponse.Status.NotFound) return NotFound(new { response.StatusText });


        return StatusCode(StatusCodes.Status500InternalServerError, new { response.StatusText });
    }

    // GET api/<TimeRegisterController>/5
    [HttpGet("{timeRegistrationId:guid}")]
    public string GetById(Guid customerId, Guid projectId, Guid timeRegistrationId)
    {
        return "Heyy";
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(Guid customerId, Guid projectId, [FromBody] ApiCreateTimeRegisterCommand.CreateTimeRegisterModel model)
    {
        var response = await _mediator.Send(new ApiCreateTimeRegisterCommand(customerId: customerId, projectId: projectId, model: model));

        if (response.StatusCode == IResponse.Status.Success)
            return CreatedAtAction(nameof(GetById), new { CustomerId = customerId, ProjectId = projectId, TimeRegistrationId = response.TimeRegistration!.Id },
                response.TimeRegistration);

        if (response.StatusCode == IResponse.Status.NotFound) return NotFound(new { response.StatusText });


        if (response.Errors is null || response.Errors.Count < 1)
        {
            return BadRequest(new { Status = response.StatusText });
        }


        var errors = new List<string>();
        response.Errors.ForEach(x => errors.Add(x.ErrorMessage));
        return BadRequest(new { response.StatusText, Errors = errors });
    }


    // PUT api/<TimeRegisterController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value, Guid customerId, Guid projectId)
    {
    }

    // DELETE api/<TimeRegisterController>/5
    [HttpDelete("{id}")]
    public void Delete(int id, Guid customerId, Guid projectId)
    {
    }
}
