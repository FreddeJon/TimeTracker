namespace Application.Features.API.Projects.Query.ApiGetProjectById;
public class ApiGetProjectByIdQuery : IRequest<ApiGetProjectByIdResponse>
{
    public Guid CustomerId { get; }
    public Guid ProjectId { get; }

    public ApiGetProjectByIdQuery(Guid customerId, Guid projectId)
    {
        CustomerId = customerId;
        ProjectId = projectId;
    }
}

public class ApiGetProjectByIdResponse : BaseResponse
{
    public ApiGetProjectByIdQueryHandler.ProjectDto Project { get; set; }
}

public class ApiGetProjectByIdQueryHandler : IRequestHandler<ApiGetProjectByIdQuery, ApiGetProjectByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly TimeTrackerContext _context;

    public ApiGetProjectByIdQueryHandler(IMapper mapper, TimeTrackerContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<ApiGetProjectByIdResponse> Handle(ApiGetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new ApiGetProjectByIdResponse();

        try
        {
            var customerFound = await _context.Customers.AnyAsync(x => x.Id == request.CustomerId, cancellationToken: cancellationToken);
            if (!customerFound)
            {
                response.StatusCode = IResponse.Status.NotFound;
                response.StatusText = "Customer not found";
                return response;
            }

            var project = await _mapper.ProjectTo<ProjectDto>(
                _context.Projects.Where(x => x.Customer.Id == request.CustomerId)
            ).FirstOrDefaultAsync(x => x.Id == request.ProjectId, cancellationToken: cancellationToken);


            if (project is null)
            {
                response.StatusCode = IResponse.Status.NotFound;
                response.StatusText = "Project not found";
                return response; ;
            }

            response.Project = project;
        }
        catch (Exception e)
        {
            response.StatusCode = IResponse.Status.Error;
            response.StatusText = "Failed to get item from database";
        }

        return response;
    }

    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; } = null!;
    }
}
