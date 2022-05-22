namespace Application.Features.API.Projects.Query.ApiGetProjects;

public class ApiGetProjectsForCustomerPaginatedQueryHandler : IRequestHandler<ApiGetProjectsForCustomerPaginatedQuery, ApiGetProjectsForCustomerPaginatedResponse>
{
    private readonly IMapper _mapper;
    private readonly TimeTrackerContext _context;

    public ApiGetProjectsForCustomerPaginatedQueryHandler(IMapper mapper, TimeTrackerContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<ApiGetProjectsForCustomerPaginatedResponse> Handle(ApiGetProjectsForCustomerPaginatedQuery request, CancellationToken cancellationToken)
    {
        var response = new ApiGetProjectsForCustomerPaginatedResponse();

        try
        {
            var customer = await _context.Customers.FindAsync(request.CustomerId);

            if (customer is null)
            {
                response.StatusCode = IResponse.Status.NotFound;
                response.StatusText = "Customer not found";
                return response;
            }

            var projects = await _mapper.ProjectTo<ProjectDto>(_context.Projects
                .Where(x => x.Customer.Id == request.CustomerId).Skip(request.Offset)
                .Take(request.Limit)).ToListAsync(cancellationToken: cancellationToken);

            response.Customer = _mapper.Map<CustomerDto>(customer);
            response.Projects = projects;
            response.TotalCount = await _context.Projects.Where(x => x.Customer.Id == request.CustomerId).CountAsync(cancellationToken: cancellationToken);
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

    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; } = null!;
    }
}
