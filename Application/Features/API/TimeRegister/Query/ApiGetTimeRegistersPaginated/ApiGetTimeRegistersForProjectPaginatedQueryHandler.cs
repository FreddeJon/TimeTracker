namespace Application.Features.API.TimeRegister.Query.ApiGetTimeRegistersPaginated;

public class ApiGetTimeRegistersForProjectPaginatedQueryHandler : IRequestHandler<ApiGetTimeRegistersForProjectPaginatedQuery, ApiGetTimeRegistersResponse>
{
    private readonly IMapper _mapper;
    private readonly TimeTrackerContext _context;

    public ApiGetTimeRegistersForProjectPaginatedQueryHandler(IMapper mapper, TimeTrackerContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<ApiGetTimeRegistersResponse> Handle(ApiGetTimeRegistersForProjectPaginatedQuery request, CancellationToken cancellationToken)
    {
        var response = new ApiGetTimeRegistersResponse();

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

            var registers = await _mapper.ProjectTo<TimeRegistrationDto>(_context.TimeRegistrations
                .Where(x => x.Project.Id == request.ProjectId).OrderByDescending(x => x.Date).Skip(request.Offset).Take(request.Limit))
                .ToListAsync(cancellationToken: cancellationToken);


            response.TimeRegistrations = registers;
            response.TotalCount =
                await _context.TimeRegistrations.Where(x => x.Project.Id == request.ProjectId).CountAsync(cancellationToken: cancellationToken);
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
