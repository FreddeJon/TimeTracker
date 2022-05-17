namespace Application.Features.API.Customers.Commands.ApiEditCustomer;

public class ApiEditCustomerResponse : BaseResponse
{
    public List<ValidationFailure>? Errors { get; set; }
    public ApiEditCustomerCommandHandler.CustomerDto? Customer { get; set; }
}
