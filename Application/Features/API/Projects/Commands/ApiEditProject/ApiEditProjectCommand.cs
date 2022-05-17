namespace Application.Features.API.Projects.Commands.ApiEditProject;
public class ApiEditProjectCommand : IRequest<ApiEditProjectResponse>
{
    public Guid CustomerId { get; }
    public Guid ProjectId { get; }
    public string ProjectName { get; }

    public ApiEditProjectCommand(Guid customerId, Guid projectId, string projectName)
    {
        CustomerId = customerId;
        ProjectId = projectId;
        ProjectName = projectName;
    }
}
