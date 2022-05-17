namespace Application.Features.Customer.Command.CreateCustomer;

public class CreateCustomerResponse : BaseResponse
{
    public CustomerDto Customer { get; set; }
    public List<ValidationFailure> Errors { get; set; }
}
