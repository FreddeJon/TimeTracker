using Application.Contracts.Responses;

namespace Application.Features.Customer.Query.GetCustomerById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerResponse>
{
    private readonly IMapper _mapper;
    private readonly TimeTrackerContext _context;

    public GetCustomerByIdQueryHandler(IMapper mapper, TimeTrackerContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<GetCustomerResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new GetCustomerResponse();
        var customer = await _context.Customers.FindAsync(request.CustomerId);

        if (customer is null)
        {
            response.StatusCode = IResponse.Status.Error;
            response.StatusText = "Customer not found";
        }

        response.Customer = _mapper.Map<CustomerDto>(customer);

        return response;
    }
}