namespace Application.Features.API.Projects.Commands.ApiCreateProject;

public class ApiCreateProjectValidator : AbstractValidator<ApiCreateProjectCommand>
{
    public ApiCreateProjectValidator()
    {
        RuleFor(x => x.CustomerId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required");
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(40).WithMessage("{PropertyName} max length  is {MaxLength}");
    }
}