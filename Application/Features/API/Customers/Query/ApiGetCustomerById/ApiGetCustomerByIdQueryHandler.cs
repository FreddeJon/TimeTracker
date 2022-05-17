namespace Application.Features.API.Customers.Query.ApiGetCustomerById;

public class ApiGetCustomerByIdQueryHandler : IRequestHandler<ApiGetCustomerByIdQuery, ApiGetCustomerResponse>
{
    private readonly IMapper _mapper;
    private readonly TimeTrackerContext _context;

    public ApiGetCustomerByIdQueryHandler(IMapper mapper, TimeTrackerContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ApiGetCustomerResponse> Handle(ApiGetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new ApiGetCustomerResponse();

        try
        {
            var customer = _mapper.Map<CustomerDto>(await _context.Customers.FindAsync(request.CustomerId));

            if (customer is null)
            {
                response.StatusCode = IResponse.Status.Error;
                response.StatusText = "Customer not found";
                return response;
            }

            response.Customer = customer;
        }
        catch (Exception)
        {
            response.StatusCode = IResponse.Status.Error;
            response.StatusText = "Could not load from database";
        }
        return response;
    }


    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}