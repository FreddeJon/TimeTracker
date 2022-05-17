namespace Application.Features.API.Projects.Commands.ApiCreateProject;

public class ApiCreateProjectCommandHandler : IRequestHandler<ApiCreateProjectCommand, ApiCreateProjectResponse>
{
    private readonly IMapper _mapper;
    private readonly TimeTrackerContext _context;

    public ApiCreateProjectCommandHandler(IMapper mapper, TimeTrackerContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<ApiCreateProjectResponse> Handle(ApiCreateProjectCommand request, CancellationToken cancellationToken)
    {
        var response = new ApiCreateProjectResponse();
        var validator = new ApiCreateProjectValidator();

        var result = await validator.ValidateAsync(request, cancellationToken);

        if (!result.IsValid)
        {
            response.StatusCode = IResponse.Status.Error;
            response.StatusText = "Validation failed, check errors";
            response.Errors = result.Errors;
            return response;
        }

        try
        {
            var customer = await _context.Customers.FindAsync(new object?[] { request.CustomerId }, cancellationToken: cancellationToken);

            if (customer is null)
            {
                response.StatusCode = IResponse.Status.NotFound;
                response.StatusText = "Customer not found";
                return response;
            }

            var newProject = new Project
            {
                ProjectName = request.Name,
            };

            customer.Projects.Add(newProject);

            _context.Entry(customer).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken);

            response.Project = _mapper.Map<ProjectDto>(newProject);
        }
        catch (Exception)
        {
            response.StatusCode = IResponse.Status.Error;
            response.StatusText = "Failed to save to database";
        }

        return response;
    }

    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; } = null!;
    }


}
