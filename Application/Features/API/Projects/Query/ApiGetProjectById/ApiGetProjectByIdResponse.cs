namespace Application.Features.API.Projects.Query.ApiGetProjectById;

public class ApiGetProjectByIdResponse : BaseResponse
{
    public ApiGetProjectByIdQueryHandler.ProjectDto Project { get; set; }
    public ApiGetProjectByIdQueryHandler.CustomerDto Customer { get; set; }
}
