namespace Application.Features.API.Projects.Query.ApiGetProjectById;
public class ApiGetProjectByIdQuery : IRequest<ApiGetProjectByIdResponse>
{
    public Guid CustomerId { get; }
    public Guid ProjectId { get; }

    public ApiGetProjectByIdQuery(Guid customerId, Guid projectId)
    {
        CustomerId = customerId;
        ProjectId = projectId;
    }
}
