namespace Application.Features.Customer.Query.GetCustomerById;

public class GetCustomerResponse : BaseResponse
{
    public CustomerDto Customer { get; set; }
}