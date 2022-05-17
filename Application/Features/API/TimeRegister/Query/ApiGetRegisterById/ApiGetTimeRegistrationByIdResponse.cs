namespace Application.Features.API.TimeRegister.Query.ApiGetRegisterById;

public class ApiGetTimeRegistrationByIdResponse : BaseResponse
{
    public ApiGetTimeRegistrationByIdQueryHandler.TimeRegistrationDto? TimeRegistrations { get; set; }
}