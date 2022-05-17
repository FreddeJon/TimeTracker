namespace Application.Features.API.TimeRegister.Commands.ApiEditTimeRegistration;

public class ApiEditTimeRegistrationResponse : BaseResponse
{
    public List<ValidationFailure>? Errors { get; set; }
    public ApiEditTimeRegistrationCommandHandler.TimeRegistrationDto? TimeRegistration { get; set; }
}