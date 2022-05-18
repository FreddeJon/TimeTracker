namespace Application.Features.API.Customers.Commands.ApiEditCustomer;

public class ApiEditCustomerValidator : AbstractValidator<ApiEditCustomerCommand>
{
    public ApiEditCustomerValidator()
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
