namespace Application.Features.API.Projects.Commands.ApiCreateProject;
public class ApiCreateProjectCommand : IRequest<ApiCreateProjectResponse>
{
    public ApiCreateProjectCommand(Guid customerId, string name)
    {
        CustomerId = customerId;
        Name = name;
    }

    public string Name { get; }

    public Guid CustomerId { get; }
}
