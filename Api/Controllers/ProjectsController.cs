namespace Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll(Guid customerId, int limit = 20, int offset = 0)
    {
        limit = limit is < 0 or > 50 ? 20 : limit;
        offset = offset < 0 ? 0 : offset;

        return Ok("Hey");
    }



    [HttpGet("{projectId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid projectId)
    {
        // Return customer and projects
        return Ok(projectId);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create()
    {
        return Ok("Hey");
    }


    [HttpPut("{projectId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(Guid projectId)
    {
        return Ok("Hey");
    }
}
