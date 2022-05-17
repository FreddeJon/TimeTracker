namespace Application.Features.API.TimeRegister.Commands.ApiEditTimeRegistration;

public class ApiEditTimeRegistrationCommandHandler : IRequestHandler<ApiEditTimeRegistrationCommand, ApiEditTimeRegistrationResponse>
{
    private readonly IMapper _mapper;
    private readonly TimeTrackerContext _context;

    public ApiEditTimeRegistrationCommandHandler(IMapper mapper, TimeTrackerContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<ApiEditTimeRegistrationResponse> Handle(ApiEditTimeRegistrationCommand request, CancellationToken cancellationToken)
    {
        var response = new ApiEditTimeRegistrationResponse();
        var validator = new ApiEditTimeRegistrationValidator();
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

            var registration = await _context.TimeRegistrations.Where(x => x.Project.Id == request.ProjectId)
                .FirstOrDefaultAsync(x => x.Id == request.TimeRegistrationId, cancellationToken: cancellationToken);

            if (registration is null)
            {
                response.StatusCode = IResponse.Status.NotFound;
                response.StatusText = "TimeRegistration not found";
                return response;
            }

            _context.Entry(registration).CurrentValues.SetValues(request.Model);
            await _context.SaveChangesAsync(cancellationToken);

            response.TimeRegistration = _mapper.Map<TimeRegistrationDto>(registration);

        }
        catch (Exception)
        {
            response.StatusCode = IResponse.Status.Error;
            response.StatusText = "Failed to save to database";
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