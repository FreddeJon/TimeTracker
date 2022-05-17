

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
    public IActionResult GetAllForCustomer(Guid customerId, Guid projectId)
    {

        return Ok("Hey");
    }

    // GET api/<TimeRegisterController>/5
    [HttpGet("{id}")]
    public string Get(int id, Guid customerId, Guid projectId)
    {
        return "value";
    }

    // POST api/<TimeRegisterController>
    [HttpPost]
    public void Post([FromBody] string value, Guid customerId, Guid projectId)
    {
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
