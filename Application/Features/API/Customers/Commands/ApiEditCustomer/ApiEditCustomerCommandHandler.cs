namespace Application.Features.API.Customers.Commands.ApiEditCustomer;

public class ApiEditCustomerCommandHandler : IRequestHandler<ApiEditCustomerCommand, ApiEditCustomerResponse>
{
    private readonly IMapper _mapper;
    private readonly TimeTrackerContext _context;

    public ApiEditCustomerCommandHandler(IMapper mapper, TimeTrackerContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<ApiEditCustomerResponse> Handle(ApiEditCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = new ApiEditCustomerResponse();
        var validator = new ApiEditCustomerValidator();

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
            var customer = await _context.Customers.FindAsync(request.CustomerId);

            if (customer is null)
            {
                response.StatusCode = IResponse.Status.NotFound;
                response.StatusText = "Customer not found";
                return response;
            }

            customer.Name = request.Name;
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);


            response.Customer = _mapper.Map<CustomerDto>(customer);
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
