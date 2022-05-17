namespace Application.Features.API.TimeRegister.Query.ApiGetRegisterById;
public class ApiGetTimeRegistrationByIdQuery : IRequest<ApiGetTimeRegistrationByIdResponse>
{
    public Guid CustomerId { get; }
    public Guid ProjectId { get; }
    public Guid TimeRegistrationId { get; }

    public ApiGetTimeRegistrationByIdQuery(Guid customerId, Guid projectId, Guid timeRegistrationId)
    {
        CustomerId = customerId;
        ProjectId = projectId;
        TimeRegistrationId = timeRegistrationId;
    }
}
