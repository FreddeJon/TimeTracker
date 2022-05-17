namespace Application.Features.API.Projects.Commands.ApiCreateProject;

public class ApiCreateProjectResponse : BaseResponse
{
    public ApiCreateProjectCommandHandler.ProjectDto? Project { get; set; }
    public List<ValidationFailure>? Errors { get; set; }
}