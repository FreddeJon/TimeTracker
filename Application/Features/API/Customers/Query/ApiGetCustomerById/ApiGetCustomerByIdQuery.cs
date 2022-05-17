namespace Application.Features.API.Customers.Query.ApiGetCustomerById;
public class ApiGetCustomerByIdQuery : IRequest<ApiGetCustomerResponse>
{
    public ApiGetCustomerByIdQuery(Guid customerId)
    {
        CustomerId = customerId;
    }

    public Guid CustomerId { get; private set; }
}

public class ApiGetCustomerResponse : BaseResponse
{
}

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
            var customer = await _mapper.ProjectTo<CustomerDto>(_context.Customers.);
        }
        catch (Exception e)
        {

        }




        return response;
    }

    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
