using Application.Contracts.Responses;
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
                new ApiGetProjectsForCustomerPaginatedQuery(customerId: customerId, limit: limit, offset: offset));

        if (response.StatusCode == IResponse.Status.Success) return Ok(new
        {
            Data = response.Projects,
            response.TotalCount,
            response.StatusText
        });

        return response.StatusCode == IResponse.Status.NotFound ? NotFound(response.StatusText) :
            StatusCode(StatusCodes.Status500InternalServerError, response.StatusText);
    }

    [HttpGet("{projectId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid customerId, Guid projectId)
    {
        var response = await _mediator.Send(new ApiGetProjectByIdQuery(customerId, projectId));


        if (response.StatusCode == IResponse.Status.Success)
            return Ok(response.Project);

        return response.StatusCode == IResponse.Status.NotFound ? NotFound(response.StatusText)
            : StatusCode(StatusCodes.Status500InternalServerError, response.StatusText);
    }



    public class CreateProjectModel
    {
        [Required]
        [MaxLength(50)]
        public string ProjectName { get; set; } = null!;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(Guid customerId, [FromBody] CreateProjectModel model)
    {
        var response = await _mediator.Send(new ApiCreateProjectCommand(customerId: customerId, name: model.ProjectName));

        if (response.StatusCode == IResponse.Status.Success)
            return CreatedAtAction(nameof(GetById), new { CustomerId = customerId, ProjectId = response.Project!.Id },
                response.Project);

        if (response.StatusCode == IResponse.Status.NotFound) return NotFound(new { response.StatusText });


        return BadRequest(new { response.StatusText });
    }



    public class EditProjectModel
    {
        [Required]
        [MaxLength(50)]
        public string ProjectName { get; set; } = null!;
    }

    [HttpPut("{projectId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Edit(Guid customerId, Guid projectId, [FromBody] EditProjectModel model)
    {
        var response = await _mediator.Send(new ApiEditProjectCommand(customerId: customerId, projectId: projectId,
            projectName: model.ProjectName));

        if (response.StatusCode == IResponse.Status.Success) return Ok(response.Project);


        return response.StatusCode == IResponse.Status.NotFound ? NotFound(new { response.StatusText }) :
            StatusCode(StatusCodes.Status500InternalServerError, response.StatusText);
    }
}
