namespace Application.Features.API.Customers.Query.ApiGetCustomers;

public class ApiGetCustomersResponse : BaseResponse
{
    public List<ApiGetCustomersPaginatedQueryHandler.CustomerDto> Customers { get; set; }
    public int TotalCount { get; set; }
}