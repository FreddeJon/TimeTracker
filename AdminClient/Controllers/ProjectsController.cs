namespace AdminClient.Controllers;

[Route("customers/{customerId:guid}/[controller]")]
public class ProjectsController : Controller
{
    private readonly IApiService _apiService;
    private readonly IMapper _mapper;

    public ProjectsController(IMapper mapper, IApiService apiService)
    {
        _mapper = mapper;
        _apiService = apiService;
    }


    [HttpGet("{page:int?}")]
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


        return View(new IndexProjectsViewModel
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
    public async Task<ActionResult> Create(Guid customerId)
    {
        var client = await _apiService.GetClient(HttpContext);

        var httpResponse = await client.GetAsync($"customers/{customerId}");

        if (!httpResponse.IsSuccessStatusCode)
        {
            // TODO add error message
            return RedirectToAction(nameof(Index));
        }

        var response = await httpResponse.Content.ReadFromJsonAsync<GetDetailsResponse>();


        return View(new CreateProjectViewModel { CustomerName = response?.Customer.Name });
    }

    [HttpPost(nameof(Create))]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Guid customerId, CreateProjectViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var client = await _apiService.GetClient(HttpContext);
        var httpResponse = await client.PostAsJsonAsync($"customers/{customerId}/projects", new { model.ProjectName });

        if (httpResponse.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index), "Projects", new { customerId });
        }

        ModelState.AddModelError("Error", "Something went wrong");
        return View(model);
    }

    [HttpGet("{projectId:guid}/Edit")]
    public async Task<ActionResult> Edit(Guid customerId, Guid projectId)
    {
        var client = await _apiService.GetClient(HttpContext);

        var httpResponse = await client.GetAsync($"customers/{customerId}/Projects/{projectId}");

        if (!httpResponse.IsSuccessStatusCode)
        {
            // TODO add error message
            return RedirectToAction(nameof(Index), new { customerId });
        }

        var response = await httpResponse.Content.ReadFromJsonAsync<EditProjectResponse>();


        return View(_mapper.Map<EditProjectViewModel>(response?.Project));
    }

    [HttpPost("{projectId:guid}/Edit")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(Guid customerId, Guid projectId, EditProjectViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var client = await _apiService.GetClient(HttpContext);
        var httpResponse =
            await client.PutAsJsonAsync($"customers/{customerId}/projects/{projectId}", new { model.ProjectName });

        if (httpResponse.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index), "Projects", new { customerId });
        }

        ModelState.AddModelError("Error", "Something went wrong");
        return View(model);
    }

    public class GetDetailsResponse
    {
        [JsonPropertyName("customer")] public CustomerDto Customer { get; set; }
    }

    public class EditProjectResponse
    {
        [JsonPropertyName("project")] public ProjectDto Project { get; set; }
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
}
