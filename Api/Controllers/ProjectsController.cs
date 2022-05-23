using Application.Features.API.Projects.Commands.ApiCreateProject;
using Application.Features.API.Projects.Commands.ApiEditProject;
using Application.Features.API.Projects.Query.ApiGetProjectById;
using Application.Features.API.Projects.Query.ApiGetProjects;

namespace Api.Controllers;

[Route("api/Customers/{customerId:guid}/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(Guid customerId, int limit = 20, int offset = 0)
    {
        var response =
            await _mediator.Send(
                new ApiGetProjectsForCustomerPaginatedQuery(customerId, limit, offset));

        if (response.StatusCode == IResponse.Status.Success)
        {
            return Ok(new
            {
                response.Customer,
                Data = response.Projects,
                response.TotalCount,
                limit,
                offset,
                response.StatusText
            });
        }

        return response.StatusCode == IResponse.Status.NotFound
            ? NotFound(new {response.StatusText})
            : StatusCode(StatusCodes.Status500InternalServerError, new {response.StatusText});
    }

    [HttpGet("{projectId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid customerId, Guid projectId)
    {
        var response = await _mediator.Send(new ApiGetProjectByIdQuery(customerId, projectId));


        if (response.StatusCode == IResponse.Status.Success)
        {
            return Ok(new {response.Project, response.Customer});
        }

        return response.StatusCode == IResponse.Status.NotFound
            ? NotFound(new {response.StatusText})
            : StatusCode(StatusCodes.Status500InternalServerError, new {response.StatusText});
    }

    [HttpPost]
    [Authorize("admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(Guid customerId, [FromBody] CreateProjectModel model)
    {
        var response = await _mediator.Send(new ApiCreateProjectCommand(customerId, model.ProjectName));

        if (response.StatusCode == IResponse.Status.Success)
        {
            return CreatedAtAction(nameof(GetById), new {CustomerId = customerId, ProjectId = response.Project!.Id},
                new {response.Project});
        }

        if (response.StatusCode == IResponse.Status.NotFound)
        {
            return NotFound(new {response.StatusText});
        }

        if (response.Errors is null || response.Errors.Count < 1)
        {
            return BadRequest(new {Status = response.StatusText});
        }


        var errors = new List<string>();
        response.Errors.ForEach(x => errors.Add(x.ErrorMessage));
        return BadRequest(new {response.StatusText, Errors = errors});
    }

    [HttpPut("{projectId:guid}")]
    [Authorize("admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Edit(Guid customerId, Guid projectId, [FromBody] EditProjectModel model)
    {
        var response = await _mediator.Send(new ApiEditProjectCommand(customerId, projectId,
            model.ProjectName));

        if (response.StatusCode == IResponse.Status.Success)
        {
            return NoContent();
        }


        return response.StatusCode == IResponse.Status.NotFound
            ? NotFound(new {response.StatusText})
            : StatusCode(StatusCodes.Status500InternalServerError, new {response.StatusText});
    }


    public class CreateProjectModel
    {
        [Required] [MaxLength(50)] public string ProjectName { get; set; } = null!;
    }

    public class EditProjectModel
    {
        [Required] [MaxLength(50)] public string ProjectName { get; set; } = null!;
    }
}
