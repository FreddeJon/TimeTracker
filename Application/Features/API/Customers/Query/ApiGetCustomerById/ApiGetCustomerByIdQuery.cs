namespace Application.Features.API.Customers.Query.ApiGetCustomerById;
public class ApiGetCustomerByIdQuery : IRequest<ApiGetCustomerResponse>
{
    public ApiGetCustomerByIdQuery(Guid customerId)
    {
        CustomerId = customerId;
    }

    public Guid CustomerId { get; private set; }
}
