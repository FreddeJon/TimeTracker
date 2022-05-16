namespace Application.Features.Customer.Command.CreateCustomer;
public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(40).WithMessage("{PropertyName} max length  is {MaxLength}");
    }
}
