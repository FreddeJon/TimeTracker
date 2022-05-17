namespace Application.Features.API.Customers.Query.ApiGetCustomerById;

public class ApiGetCustomerResponse : BaseResponse
{
    public ApiGetCustomerByIdQueryHandler.CustomerDto Customer { get; set; }
}