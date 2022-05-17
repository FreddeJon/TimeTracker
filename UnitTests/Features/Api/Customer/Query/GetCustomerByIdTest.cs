using Application.Contracts.Responses;
using Application.Features.API.Customers.Query.ApiGetCustomerById;

namespace UnitTests.Features.Api.Customer.Query;
public class GetCustomerByIdTest
{
    private readonly TimeTrackerContext _context;
    private readonly ApiGetCustomerByIdQueryHandler _sut;

    public GetCustomerByIdTest()
    {
        var options = new DbContextOptionsBuilder<TimeTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<Profiles>();
        });

        var mapper = configurationProvider.CreateMapper();

        _context = new TimeTrackerContext(options);
        _sut = new ApiGetCustomerByIdQueryHandler(mapper, _context);


        _context.SeedCustomers();
    }

    [Fact]
    public async Task Valid_id_should_get_customer()
    {
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39225");
        var response = await _sut.Handle(new ApiGetCustomerByIdQuery(customerId), new CancellationToken());

        response.ShouldBeOfType<ApiGetCustomerResponse>();

        response.StatusText.ShouldBe("Success");
        response.Customer.Name.ShouldBe("Apple");
    }


    [Fact]
    public async Task InValid_id_should_not_get_customer()
    {
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39290");
        var response = await _sut.Handle(new ApiGetCustomerByIdQuery(customerId), new CancellationToken());

        response.ShouldBeOfType<ApiGetCustomerResponse>();

        response.StatusCode.ShouldBe(IResponse.Status.Error);
        response.Customer.ShouldBeNull();
    }
}
