namespace Application.Features.API.TimeRegister.Query.ApiGetTimeRegistersPaginated;
public class ApiGetTimeRegistersForProjectPaginatedQuery : IRequest<ApiGetTimeRegistersResponse>
{
    public ApiGetTimeRegistersForProjectPaginatedQuery(Guid customerId, Guid projectId, int limit = 20, int offset = 0)
    {
        CustomerId = customerId;
        ProjectId = projectId;
        Limit = limit is < 0 or > 50 ? 20 : limit;
        Offset = offset < 0 ? 0 : offset;
    }

    public int Offset { get; }
    public int Limit { get; }
    public Guid CustomerId { get; }
    public Guid ProjectId { get; }
}
