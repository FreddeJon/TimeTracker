namespace Application.Features.API.TimeRegister.Commands.ApiEditTimeRegistration;

public class ApiEditTimeRegistrationValidator : AbstractValidator<ApiEditTimeRegistrationCommand>
{
    public ApiEditTimeRegistrationValidator()
    {
        RuleFor(x => x.CustomerId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required");
        RuleFor(x => x.ProjectId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required");
        RuleFor(x => x.TimeRegistrationId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required");
        RuleFor(x => x.Model.Date)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Date is required")
            .Must(CorrectTime).WithMessage("Invalid date");
        RuleFor(x => x.Model.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(300).WithMessage("Description max length {MaxLength}");
        RuleFor(x => x.Model.TimeInMinutes)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("TimeInMinutes is required")
            .GreaterThanOrEqualTo(1).WithMessage("TimeInMinutes must be greater then 1")
            .LessThanOrEqualTo(1440).WithMessage("TimeInMinutes must be less then 1440");
    }

    private static bool CorrectTime(DateTime date)
    {
        return date >= DateTime.Now.AddDays(-30) && date <= DateTime.Now;
    }
}