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
            var customerFound = await _context.Customers.AnyAsync(x => x.Id == request.CustomerId, cancellationToken: cancellationToken);

            if (!customerFound)
            {
                response.StatusCode = IResponse.Status.NotFound;
                response.StatusText = "Customer not found";
                return response;
            }

            var projects = await _mapper.ProjectTo<DTO.ProjectDto>(_context.Projects
                .Where(x => x.Customer.Id == request.CustomerId).Skip(request.Offset)
                .Take(request.Limit)).ToListAsync(cancellationToken: cancellationToken);

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


    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; } = null!;
    }
}