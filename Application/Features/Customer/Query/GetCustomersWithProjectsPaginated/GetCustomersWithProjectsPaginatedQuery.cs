namespace Application.Features.Customer.Query.GetCustomersWithProjectsPaginated;

public class GetCustomersWithProjectsPaginatedQuery : IRequest<Response>
{
    public GetCustomersWithProjectsPaginatedQuery(int limit = 20, int offset = 0)
    {
        Limit = limit is < 0 or > 50 ? 20 : limit;
        Offset = offset < 0 ? 0 : offset;
    }

    public int Limit { get; private set; }
    public int Offset { get; private set; }
}

public class Response : BaseResponse
{
    public List<CustomerDto> Customers { get; set; }
    public int TotalCount { get; set; }
}
