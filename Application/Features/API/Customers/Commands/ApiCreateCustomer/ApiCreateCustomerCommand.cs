namespace Application.Features.API.Customers.Commands.ApiCreateCustomer;
public class ApiCreateCustomerCommand : IRequest<ApiCreateCustomerResponse>
{
    public ApiCreateCustomerCommand(string name)
    {
        Name = name;
    }

    public string Name { get; }
}
