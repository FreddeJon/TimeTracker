namespace Api.Controllers;
[Route("api/customers/{customerId:guid}/")]
[ApiController]
public class TimeTrackerController : ControllerBase
{

    [HttpGet("timetracker")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllForCustomer(Guid customerId)
    {
        return Ok("Hey");
    }


    [HttpGet("project/{projectId:guid}/timetracker")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllForProject(Guid customerId)
    {
        return Ok("Hey");
    }


    [HttpGet("timetracker/{trackerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid customerId, Guid trackerId)
    {
        return Ok(customerId);
    }



    [HttpPost("project/{projectId:guid}/timetracker")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create(Guid customerId)
    {
        return Ok("Hey");
    }


    //[HttpPut("{customerId:guid}")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public IActionResult Update(Guid customerId)
    //{
    //    return Ok("Hey");
    //}
}
