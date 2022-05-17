using Application.Contracts.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.API.Customers.Query.ApiGetCustomers;

public class ApiGetCustomersPaginatedQueryHandler : IRequestHandler<ApiGetCustomersPaginatedQuery, ApiGetCustomersResponse>
{
    private readonly IMapper _mapper;
    private readonly TimeTrackerContext _context;

    public ApiGetCustomersPaginatedQueryHandler(IMapper mapper, TimeTrackerContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ApiGetCustomersResponse> Handle(ApiGetCustomersPaginatedQuery request, CancellationToken cancellationToken)
    {
        var response = new ApiGetCustomersResponse();

        try
        {
            var customers = await _mapper.ProjectTo<CustomerDto>(
                    _context.Customers.Skip(request.Offset).Take(request.Limit))
                .ToListAsync(cancellationToken: cancellationToken);

            response.Customers = customers;
            response.TotalCount = await _context.Customers.CountAsync(cancellationToken: cancellationToken);
        }
        catch (Exception)
        {
            response.StatusCode = IResponse.Status.Error;
            response.StatusText = "Could not load from DB";
        }

        return response;
    }


    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}