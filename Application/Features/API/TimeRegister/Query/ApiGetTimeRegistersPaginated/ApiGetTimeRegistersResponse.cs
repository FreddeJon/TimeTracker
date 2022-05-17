namespace Application.Features.API.TimeRegister.Query.ApiGetTimeRegistersPaginated;

public class ApiGetTimeRegistersResponse : BaseResponse
{
    public int? TotalCount { get; set; }
    public List<ApiGetTimeRegistersForProjectPaginatedQueryHandler.TimeRegistrationDto>? TimeRegistrations { get; set; }
}
