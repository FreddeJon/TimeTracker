namespace Application.Features.API.Projects.Commands.ApiEditProject;

public class ApiEditProjectValidator : AbstractValidator<ApiEditProjectCommand>
{
    public ApiEditProjectValidator()
    {
        RuleFor(x => x.CustomerId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required");
        RuleFor(x => x.ProjectId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required");
        RuleFor(x => x.ProjectName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(50).WithMessage("{PropertyName} max length  is {MaxLength}");
    }
}