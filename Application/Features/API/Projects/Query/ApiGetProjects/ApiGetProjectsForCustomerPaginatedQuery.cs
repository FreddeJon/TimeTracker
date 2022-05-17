namespace Application.Features.API.Projects.Query.ApiGetProjects;
public class ApiGetProjectsForCustomerPaginatedQuery : IRequest<ApiGetProjectsForCustomerPaginatedResponse>
{
    public ApiGetProjectsForCustomerPaginatedQuery(Guid customerId, int limit = 20, int offset = 0)
    {
        CustomerId = customerId;
        Limit = limit is < 0 or > 50 ? 20 : limit;
        Offset = offset < 0 ? 0 : offset;
    }

    public Guid CustomerId { get; }

    public int Limit { get; }

    public int Offset { get; }
}
