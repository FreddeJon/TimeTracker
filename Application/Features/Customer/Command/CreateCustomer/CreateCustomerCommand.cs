namespace Application.Features.Customer.Command.CreateCustomer;
public class CreateCustomerCommand : IRequest<CreateCustomerResponse>
{
    public CreateCustomerCommand(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }

}
