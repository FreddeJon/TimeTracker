namespace Application.Features.Customer.Query.GetCustomersWithProjectsPaginated;
public class GetCustomersWithProjectsPaginatedQueryHandler : IRequestHandler<GetCustomersWithProjectsPaginatedQuery, Response>
{
    private readonly TimeTrackerContext _context;
    private readonly IMapper _mapper;

    public GetCustomersWithProjectsPaginatedQueryHandler(TimeTrackerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response> Handle(GetCustomersWithProjectsPaginatedQuery request, CancellationToken cancellationToken)
    {
        var response = new Response();

        var customers = _mapper.Map<List<CustomerDto>>(await _context.Customers.Include(x => x.Projects).Skip(request.Offset).Take(request.Limit).ToListAsync(cancellationToken: cancellationToken));


        response.TotalCount = await _context.Customers.CountAsync(cancellationToken: cancellationToken);
        response.Customers = customers;

        return response;
    }
}
