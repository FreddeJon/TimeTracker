namespace Application.Features.API.Customers.Commands.ApiEditCustomer;
public class ApiEditCustomerCommand : IRequest<ApiEditCustomerResponse>
{
    public Guid CustomerId { get; }
    public string Name { get; }

    public ApiEditCustomerCommand(Guid customerId, string name)
    {
        CustomerId = customerId;
        Name = name;
    }

}
