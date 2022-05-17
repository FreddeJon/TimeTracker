using Application.Contracts.Responses;

namespace Application.Features.API.Customers.Commands.ApiCreateCustomer;

public class ApiCreateCustomerCommandHandler : IRequestHandler<ApiCreateCustomerCommand, ApiCreateCustomerResponse>
{
    private readonly IMapper _mapper;
    private readonly TimeTrackerContext _context;

    public ApiCreateCustomerCommandHandler(IMapper mapper, TimeTrackerContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<ApiCreateCustomerResponse> Handle(ApiCreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = new ApiCreateCustomerResponse();
        var validator = new ApiCreateCustomerValidator();

        var result = await validator.ValidateAsync(request, cancellationToken);

        if (!result.IsValid)
        {
            response.StatusCode = IResponse.Status.Error;
            response.StatusText = "Validation failed, check errors";
            response.Errors = result.Errors;
            return response;
        }

        var newCustomer = new Domain.Entities.Customer
        {
            Name = request.Name,
        };

        try
        {
            await _context.Customers.AddAsync(newCustomer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            response.Customer = _mapper.Map<CustomerDto>(newCustomer);
        }
        catch (Exception)
        {
            response.StatusCode = IResponse.Status.Error;
            response.StatusText = "Failed to save to database";
        }

        return response;
    }


    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}