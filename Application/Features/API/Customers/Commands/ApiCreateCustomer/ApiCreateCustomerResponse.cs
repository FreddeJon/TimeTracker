using FluentValidation.Results;

namespace Application.Features.API.Customers.Commands.ApiCreateCustomer;

public class ApiCreateCustomerResponse : BaseResponse
{
    public List<ValidationFailure> Errors { get; set; }
    public ApiCreateCustomerCommandHandler.CustomerDto Customer { get; set; }
}