namespace Application.Features.API.Projects.Commands.ApiEditProject;

public class ApiEditProjectResponse : BaseResponse
{
    public List<ValidationFailure>? Error { get; set; }
    public ApiEditProjectCommandHandler.ProjectDto Project { get; set; }
}