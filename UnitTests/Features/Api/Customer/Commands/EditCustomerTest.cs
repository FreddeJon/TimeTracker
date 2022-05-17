using Application.Contracts.Responses;
using Application.Features.API.Customers.Commands.ApiCreateCustomer;
using Application.Features.API.Customers.Commands.ApiEditCustomer;

namespace UnitTests.Features.Api.Customer.Commands;
public class EditCustomerTest
{
    private readonly TimeTrackerContext _context;
    private readonly ApiEditCustomerCommandHandler _sut;
    public EditCustomerTest()
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
        _sut = new ApiEditCustomerCommandHandler(mapper, _context);


        _context.SeedCustomers();
    }



    [Fact]
    public async Task Valid_name_should_edit_customer()
    {
        const string name = "Apple, Inc";
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39225");


        var response = await _sut.Handle(new ApiEditCustomerCommand(customerId, name), new CancellationToken());


        _context.Customers.Count().ShouldBe(9);

        (await _context.Customers.FindAsync(customerId))?.Name.ShouldBe(name);


        response.ShouldBeOfType<ApiEditCustomerResponse>();
        response.StatusCode.ShouldBe(IResponse.Status.Success);
        response.Customer?.Name.ShouldBe("Apple, Inc");
    }



    [Fact]
    public async Task Valid_other_name_should_edit_customer()
    {
        const string name = "App";
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39225");


        var response = await _sut.Handle(new ApiEditCustomerCommand(customerId, name), new CancellationToken());


        _context.Customers.Count().ShouldBe(9);

        (await _context.Customers.FindAsync(customerId))?.Name.ShouldBe(name);


        response.ShouldBeOfType<ApiEditCustomerResponse>();
        response.StatusCode.ShouldBe(IResponse.Status.Success);
        response.Customer?.Name.ShouldBe(name);
    }

    [Fact]
    public async Task InValid_name_empty_should_not_edit_customer()
    {
        const string name = "";
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39225");


        var response = await _sut.Handle(new ApiEditCustomerCommand(customerId, name), new CancellationToken());


        _context.Customers.Count().ShouldBe(9);

        (await _context.Customers.FindAsync(customerId))?.Name.ShouldNotBe(name);


        response.ShouldBeOfType<ApiEditCustomerResponse>();
        response.StatusCode.ShouldBe(IResponse.Status.Error);
        response.Customer.ShouldBeNull();
    }

    [Fact]
    public async Task InValid_name_tolong_should_not_edit_customer()
    {
        const string name = "asdfasfasfasdfasfasfasdfasfasfasdfasfasfasdfasfasfasdfasfasfasdfasfasfasdfasfasfasdfasfasfasdfasfasfasdfasfasf";
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39225");


        var response = await _sut.Handle(new ApiEditCustomerCommand(customerId, name), new CancellationToken());


        _context.Customers.Count().ShouldBe(9);

        (await _context.Customers.FindAsync(customerId))?.Name.ShouldNotBe(name);


        response.ShouldBeOfType<ApiEditCustomerResponse>();
        response.StatusCode.ShouldBe(IResponse.Status.Error);
        response.Customer.ShouldBeNull();
    }


    [Fact]
    public async Task InValid_customerID_should_not_edit_customer()
    {
        const string name = "Apple, Inc";
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39290");


        var response = await _sut.Handle(new ApiEditCustomerCommand(customerId, name), new CancellationToken());


        _context.Customers.Count().ShouldBe(9);

        (await _context.Customers.FindAsync(customerId)).ShouldBeNull();


        response.ShouldBeOfType<ApiEditCustomerResponse>();
        response.StatusCode.ShouldBe(IResponse.Status.NotFound);
        response.Customer.ShouldBeNull();
    }
}
