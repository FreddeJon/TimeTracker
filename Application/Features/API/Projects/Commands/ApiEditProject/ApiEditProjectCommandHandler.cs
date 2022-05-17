namespace Application.Features.API.Projects.Commands.ApiEditProject;

public class ApiEditProjectCommandHandler : IRequestHandler<ApiEditProjectCommand, ApiEditProjectResponse>
{
    private readonly IMapper _mapper;
    private readonly TimeTrackerContext _context;

    public ApiEditProjectCommandHandler(IMapper mapper, TimeTrackerContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<ApiEditProjectResponse> Handle(ApiEditProjectCommand request, CancellationToken cancellationToken)
    {
        var response = new ApiEditProjectResponse();
        var validator = new ApiEditProjectValidator();
        var result = await validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            response.StatusCode = IResponse.Status.Error;
            response.StatusText = "Validation failed, check errors";
            response.Error = result.Errors;
        }

        try
        {
            var customerFound = await _context.Customers.AnyAsync(x => x.Id == request.CustomerId,
                cancellationToken: cancellationToken);

            if (!customerFound)
            {
                response.StatusCode = IResponse.Status.NotFound;
                response.StatusText = "Customer not found";
                return response;
            }

            var projectToUpdate = await _context.Projects.Where(x => x.Customer.Id == request.CustomerId)
                .FirstOrDefaultAsync(x => x.Id == request.ProjectId, cancellationToken: cancellationToken);


            if (projectToUpdate is null)
            {
                response.StatusCode = IResponse.Status.NotFound;
                response.StatusText = "Project not found";
                return response;
            }

            projectToUpdate.ProjectName = request.ProjectName;
            _context.Entry(projectToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            response.Project = _mapper.Map<ProjectDto>(projectToUpdate);
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
