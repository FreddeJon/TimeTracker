using Application.Features.API.Customers.Commands.ApiCreateCustomer;

namespace UnitTests.Features.Api.Customer.Commands;
public class CreateCustomerTest
{
    private readonly TimeTrackerContext _context;
    private readonly ApiCreateCustomerCommandHandler _sut;

    public CreateCustomerTest()
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
        _sut = new ApiCreateCustomerCommandHandler(mapper, _context);


        _context.SeedCustomers();
    }


    [Fact]
    public async Task Valid_name_should_create_customer()
    {
        const string name = "Apple";
        var response = await _sut.Handle(new ApiCreateCustomerCommand(name), new CancellationToken());


        _context.Customers.Count().ShouldBe(10);


        response.ShouldBeOfType<ApiCreateCustomerResponse>();
        response.StatusCode.ShouldBe(IResponse.Status.Success);
        response.Customer.Name.ShouldBe("Apple");
    }



    [Fact]
    public async Task IValid_name_empty_should_not_create_customer()
    {
        const string name = "";
        var response = await _sut.Handle(new ApiCreateCustomerCommand(name), new CancellationToken());

        _context.Customers.Count().ShouldBe(9);

        response.ShouldBeOfType<ApiCreateCustomerResponse>();
        response.StatusCode.ShouldBe(IResponse.Status.Error);
        response.Customer.ShouldBeNull();
        response.Errors.Count.ShouldBeGreaterThan(0);
    }


    [Fact]
    public async Task IValid_name_tolong_should_not_create_customer()
    {
        const string name = "asffffffffffffffffffffffffffffffffffffffffffffffffasffffffffffffffffffffffffffffffffffffffffffffffffasffffffffffffffffffffffffffffffffffffffffffffffff";
        var response = await _sut.Handle(new ApiCreateCustomerCommand(name), new CancellationToken());

        _context.Customers.Count().ShouldBe(9);

        response.ShouldBeOfType<ApiCreateCustomerResponse>();
        response.StatusCode.ShouldBe(IResponse.Status.Error);
        response.Customer.ShouldBeNull();
        response.Errors.Count.ShouldBeGreaterThan(0);
    }
}
