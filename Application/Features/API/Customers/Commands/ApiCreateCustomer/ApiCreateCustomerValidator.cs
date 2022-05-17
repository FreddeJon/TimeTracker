namespace Application.Features.API.Customers.Commands.ApiCreateCustomer;

public class ApiCreateCustomerValidator : AbstractValidator<ApiCreateCustomerCommand>
{
    public ApiCreateCustomerValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(40).WithMessage("{PropertyName} max length  is {MaxLength}");
    }
}