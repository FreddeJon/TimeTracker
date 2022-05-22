namespace AdminClient.Controllers;


[Route("customers/{customerId:guid}/[controller]")]
public class ProjectsController : Controller
{
    private readonly IMapper _mapper;
    private readonly IApiService _apiService;

    public ProjectsController(IMapper mapper, IApiService apiService)
    {
        _mapper = mapper;
        _apiService = apiService;
    }



    public class GetProjectsResponse
    {
        [JsonPropertyName("statusText")] public string StatusText { get; set; } = null!;

        [JsonPropertyName("totalCount")] public int TotalCount { get; set; }
        [JsonPropertyName("limit")] public int Limit { get; set; }
        [JsonPropertyName("offset")] public int Offset { get; set; }

        [JsonPropertyName("data")] public List<ProjectDto> Data { get; set; }
        [JsonPropertyName("customer")] public CustomerDto Customer { get; set; }
    }


    [HttpGet]
    public async Task<ActionResult> Index(Guid customerId, int page = 1)
    {
        const int limit = 10;
        var offset = page > 0 ? (page - 1) * limit : 0;

        var client = await _apiService.GetClient(HttpContext);

        var httpResponse = await client.GetAsync($"customers/{customerId}/Projects?limit={limit}&offset={offset}");
        if (httpResponse.IsSuccessStatusCode)
        {
            // TODO fix if fetch fail
        }


        var response = await httpResponse.Content.ReadFromJsonAsync<GetProjectsResponse>();






        return View(new IndexProjectsViewModel()
        {
            Customer = _mapper.Map<CustomerDto>(response.Customer),
            Projects = _mapper.Map<List<IndexProjectsViewModel.ListProjectViewModel>>(response!.Data),
            TotalProjects = response.TotalCount,
            CurrentPage = page,
            TotalPage = (int)Math.Ceiling((double)response.TotalCount / limit),
            Limit = response.Limit
        });
    }

    [HttpGet("{projectId:guid}/Details")]
    public ActionResult Details(Guid customerId, Guid projectId)
    {
        return View();
    }

    [HttpGet(nameof(Create))]
    public ActionResult Create(Guid customerId)
    {
        return View();
    }
    public class CreateProjectModel
    {
    }
    [HttpPost(nameof(Create))]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Guid customerId, [FromBody] CreateProjectModel model)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }




    public class EditProjectModel
    {
    }

    [HttpGet("{projectId:guid}/Edit")]
    public ActionResult Edit(Guid customerId, Guid projectId)
    {
        return View();
    }

    [HttpPost("{projectId:guid}/Edit")]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Guid customerId, [FromBody] EditProjectModel model)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}




