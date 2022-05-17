namespace UnitTests.Features.Api.Customer.Query;
public class GetCustomersPaginatedTest
{
    private readonly ApiGetCustomersPaginatedQueryHandler _sut;
    private readonly TimeTrackerContext _context;

    public GetCustomersPaginatedTest()
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
        _sut = new ApiGetCustomersPaginatedQueryHandler(mapper, _context);


        _context.SeedCustomers();
    }


    [Fact]
    public async Task Valid_should_get_all_customers()
    {
        var response = await _sut.Handle(new ApiGetCustomersPaginatedQuery(), new CancellationToken());

        response.ShouldBeOfType<ApiGetCustomersResponse>();
        response.Customers.Count.ShouldBe(9);
        response.TotalCount.ShouldBe(9);
    }

    [Fact]
    public async Task Valid_Offset_should_get_4_customers()
    {
        const int offset = 5;
        var response = await _sut.Handle(new ApiGetCustomersPaginatedQuery(offset: offset), new CancellationToken());

        response.ShouldBeOfType<ApiGetCustomersResponse>();
        response.Customers.Count.ShouldBe(4);
        response.TotalCount.ShouldBe(9);
    }



    [Fact]
    public async Task Valid_Offset_limit_should_get_1_customers()
    {
        const int offset = 5;
        const int limit = 1;
        var response = await _sut.Handle(new ApiGetCustomersPaginatedQuery(offset: offset, limit: limit), new CancellationToken());

        response.ShouldBeOfType<ApiGetCustomersResponse>();
        response.Customers.Count.ShouldBe(1);
        response.TotalCount.ShouldBe(9);
    }

    [Fact]
    public async Task Valid_OffsetToHigh_limit_should_get_0_customers()
    {
        const int offset = 20;
        var response = await _sut.Handle(new ApiGetCustomersPaginatedQuery(offset: offset), new CancellationToken());

        response.ShouldBeOfType<ApiGetCustomersResponse>();
        response.Customers.Count.ShouldBe(0);
        response.TotalCount.ShouldBe(9);
    }
}
