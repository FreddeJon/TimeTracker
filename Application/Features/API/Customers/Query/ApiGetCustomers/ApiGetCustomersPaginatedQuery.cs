using AutoMapper.QueryableExtensions;

namespace Application.Features.API.Customers.Query.ApiGetCustomers;
public class ApiGetCustomersPaginatedQuery : IRequest<ApiGetCustomersResponse>
{
    public ApiGetCustomersPaginatedQuery(int limit = 20, int offset = 0)
    {
        Limit = limit is < 0 or > 50 ? 20 : limit;
        Offset = offset < 0 ? 0 : offset;
    }

    public int Limit { get; private set; }
    public int Offset { get; private set; }
}
