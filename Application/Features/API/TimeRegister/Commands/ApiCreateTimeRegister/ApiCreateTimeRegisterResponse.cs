namespace Application.Features.API.TimeRegister.Commands.ApiCreateTimeRegister;

public class ApiCreateTimeRegisterResponse : BaseResponse
{
    public List<ValidationFailure>? Errors { get; set; }
    public ApiCreateTimeRegisterCommandHandler.TimeRegistrationDto? TimeRegistration { get; set; }
}