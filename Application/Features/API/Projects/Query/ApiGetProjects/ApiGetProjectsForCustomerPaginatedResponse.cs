namespace Application.Features.API.Projects.Query.ApiGetProjects;

public class ApiGetProjectsForCustomerPaginatedResponse : BaseResponse
{
    public int TotalCount { get; set; }
    public List<ProjectDto>? Projects { get; set; }
}