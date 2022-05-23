// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable UnusedAutoPropertyAccessor.Local

#pragma warning disable CS8618
namespace AdminClient.Controllers;

[Authorize(Roles = "Admin")]
[Route("[controller]")]
public class CustomersController : Controller
{
    private readonly IApiService _apiService;
    private readonly IMapper _mapper;


    public CustomersController(IMapper mapper, IApiService apiService)
    {
        _mapper = mapper;
        _apiService = apiService;
    }


    [HttpGet]
    public async Task<ActionResult> Index(int page = 1)
    {
        const int limit = 10;
        var offset = page > 0 ? (page - 1) * limit : 0;

        var client = await _apiService.GetClient(HttpContext);


        var httpResponse = await client.GetAsync($"customers?limit={limit}&offset={offset}");

        var response = await httpResponse.Content.ReadFromJsonAsync<GetCustomersResponse>();


        return View(new IndexCustomersViewModel
        {
            Customers = _mapper.Map<List<IndexCustomersViewModel.ListCustomerViewModel>>(response!.Data),
            TotalCustomers = response.TotalCount,
            CurrentPage = page,
            TotalPage = (int)Math.Ceiling((double)response.TotalCount / limit),
            Limit = response.Limit
        });
    }


    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View(new CreateCustomerViewModel());
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCustomerViewModel model) //OnPost
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var client = await _apiService.GetClient(HttpContext);
        var httpResponse = await client.PostAsJsonAsync(
            "Customers", new {model.Name});


        if (httpResponse.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index), "Customers");
        }


        ModelState.AddModelError("Error", "Could not save");

        return View(model);
    }


    [HttpGet("{customerId:guid}/Edit")]
    public async Task<IActionResult> Edit(Guid customerId) //OnGet
    {
        var client = await _apiService.GetClient(HttpContext);

        var httpResponse = await client.GetAsync($"customers/{customerId}");

        if (!httpResponse.IsSuccessStatusCode)
        {
            // TODO add error message
            return RedirectToAction(nameof(Index));
        }

        var response = await httpResponse.Content.ReadFromJsonAsync<EditCustomerResponse>();


        return View(_mapper.Map<EditCustomerViewModel>(response?.Customer));
    }

    [HttpPost("{customerId:guid}/Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid customerId, EditCustomerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var client = await _apiService.GetClient(HttpContext);
        var httpResponse = await client.PutAsJsonAsync($"customers/{customerId}", new {model.Name});

        if (httpResponse.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index), "Customers");
        }

        ModelState.AddModelError("Error", "Something went wrong");
        return View(model);
    }

    private class EditCustomerResponse
    {
        [JsonPropertyName("customer")] public CustomerDto Customer { get; init; }
    }

    public class CreateCustomerResponse
    {
        [JsonPropertyName("statusText")] public string StatusText { get; set; }
    }

    private class GetCustomersResponse
    {
        [JsonPropertyName("statusText")] public string StatusText { get; set; } = null!;

        [JsonPropertyName("totalCount")] public int TotalCount { get; set; }
        [JsonPropertyName("limit")] public int Limit { get; set; }
        [JsonPropertyName("offset")] public int Offset { get; set; }

        [JsonPropertyName("data")] public List<CustomerDto> Data { get; set; }
    }
}
