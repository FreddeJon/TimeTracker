using Application.Contracts.Responses;
using Application.Features.Customer.Command.CreateCustomer;
using Application.Features.Customer.Query.GetCustomerById;
using Application.Features.Customer.Query.GetCustomersWithProjectsPaginated;
using Microsoft.AspNetCore.Mvc;

namespace AdminClient.Controllers;
public class CustomersController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CustomersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    // GET: CustomerController
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var response = await _mediator.Send(new GetCustomersWithProjectsPaginatedQuery());

        return View(new IndexCustomersViewModel { Customers = _mapper.Map<List<IndexCustomersViewModel.ListCustomerViewModel>>(response.Customers) });
    }

    [HttpGet]
    public ActionResult Details(int id)
    {
        return View();
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateCustomerViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCustomerViewModel model) //OnPost
    {
        if (!ModelState.IsValid) return View(model);

        var response = await _mediator.Send(new CreateCustomerCommand(model.Name));

        if (response.StatusCode != IResponse.Status.Error) return RedirectToAction(nameof(Index));

        response.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> Edit(Guid customerId) //OnGet
    {
        var response = await _mediator.Send(new GetCustomerByIdQuery(customerId));

        if (response.StatusCode == IResponse.Status.Error)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(_mapper.Map<EditCustomerViewModel>(response.Customer));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, EditCustomerViewModel model)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }
}
