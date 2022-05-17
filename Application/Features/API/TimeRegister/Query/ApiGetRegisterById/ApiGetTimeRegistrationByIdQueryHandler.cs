namespace Application.Features.API.TimeRegister.Query.ApiGetRegisterById;

public class ApiGetTimeRegistrationByIdQueryHandler : IRequestHandler<ApiGetTimeRegistrationByIdQuery, ApiGetTimeRegistrationByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly TimeTrackerContext _context;

    public ApiGetTimeRegistrationByIdQueryHandler(IMapper mapper, TimeTrackerContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<ApiGetTimeRegistrationByIdResponse> Handle(ApiGetTimeRegistrationByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new ApiGetTimeRegistrationByIdResponse();

        try
        {
            var customerFound = await _context.Customers.AnyAsync(x => x.Id == request.CustomerId, cancellationToken: cancellationToken);

            if (!customerFound)
            {
                response.StatusCode = IResponse.Status.NotFound;
                response.StatusText = "Customer not found";
                return response;
            }

            var projectFound = await _context.Customers.Where(x => x.Id == request.CustomerId)
                .AnyAsync(x => x.Projects.Any(p => p.Id == request.ProjectId), cancellationToken: cancellationToken);

            if (!projectFound)
            {
                response.StatusCode = IResponse.Status.NotFound;
                response.StatusText = "Project not found";
                return response;
            }

            var registration = await _mapper.ProjectTo<TimeRegistrationDto>(_context.TimeRegistrations.Where(x => x.Project.Id == request.ProjectId))
                .FirstOrDefaultAsync(x => x.Id == request.TimeRegistrationId, cancellationToken: cancellationToken);
            if (registration is null)
            {
                response.StatusCode = IResponse.Status.NotFound;
                response.StatusText = "TimeRegistration not found";

                return response;
            }

            response.TimeRegistrations = registration;
        }
        catch (Exception)
        {
            response.StatusCode = IResponse.Status.Error;
            response.StatusText = "Failed to load from database";
        }


        return response;
    }

    public class TimeRegistrationDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int TimeInMinutes { get; set; }
        public string Description { get; set; } = null!;
    }
}
