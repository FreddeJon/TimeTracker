using System.Security.Cryptography.X509Certificates;
using Application.Contracts.Responses;

namespace Application.Features.Customer.Command.CreateCustomer;
public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>
{
    private readonly TimeTrackerContext _context;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(TimeTrackerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = new CreateCustomerResponse();
        var validator = new CreateCustomerValidator();

        var result = await validator.ValidateAsync(request, cancellationToken);

        if (!result.IsValid)
        {
            response.Errors = result.Errors;
            response.StatusCode = IResponse.Status.Error;
            response.StatusText = "Validation failed, check errors";

            return response;
        }

        var customer = new Domain.Entities.Customer()
        {
            Name = request.Name,
        };



        await _context.Customers.AddAsync(customer, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        response.Customer = _mapper.Map<CustomerDto>(customer);

        return response;
    }
}
