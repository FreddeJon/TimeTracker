namespace Application.Features.API.TimeRegister.Commands.ApiCreateTimeRegister;

public class ApiCreateTimeRegisterCommandHandler : IRequestHandler<ApiCreateTimeRegisterCommand, ApiCreateTimeRegisterResponse>
{
    private readonly IMapper _mapper;
    private readonly TimeTrackerContext _context;

    public ApiCreateTimeRegisterCommandHandler(IMapper mapper, TimeTrackerContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<ApiCreateTimeRegisterResponse> Handle(ApiCreateTimeRegisterCommand request, CancellationToken cancellationToken)
    {
        var response = new ApiCreateTimeRegisterResponse();
        var validator = new ApiCreateTimeRegisterValidator();

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

            var project = await _context.Projects.FindAsync(request.ProjectId);

            var newTimeRegister = _mapper.Map<TimeRegistration>(request.Model);
            project!.TimeRegistrations.Add(newTimeRegister);

            _context.Entry(project).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken);


            response.TimeRegistration = _mapper.Map<TimeRegistrationDto>(newTimeRegister);

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
