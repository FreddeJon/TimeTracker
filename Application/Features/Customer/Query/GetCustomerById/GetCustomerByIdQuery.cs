namespace Application.Features.Customer.Query.GetCustomerById;
public class GetCustomerByIdQuery : IRequest<GetCustomerResponse>
{
    public GetCustomerByIdQuery(Guid customerId)
    {
        CustomerId = customerId;
    }

    public Guid CustomerId { get; private set; }
}
