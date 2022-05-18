using Application.Features.API.Projects.Commands.ApiCreateProject;

namespace UnitTests.Features.Api.Project.Commands;
public class CreateProjectTest
{
    private readonly TimeTrackerContext _context;
    private readonly ApiCreateProjectCommandHandler _sut;

    public CreateProjectTest()
    {
        var options = new DbContextOptionsBuilder<TimeTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ApiProfiles>();
        });

        var mapper = configurationProvider.CreateMapper();

        _context = new TimeTrackerContext(options);
        _sut = new ApiCreateProjectCommandHandler(mapper, _context);


        _context.SeedCustomers();
    }


    [Fact]
    public async Task Valid_should_create_project()
    {
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39225");
        const string name = "Testar";


        var response = await _sut.Handle(new ApiCreateProjectCommand(customerId: customerId, name: name), new CancellationToken());


        (await _context.Customers.FindAsync(customerId))?.Projects.Count.ShouldBe(3);

        response.ShouldBeOfType<ApiCreateProjectResponse>();
        response.Project?.ProjectName.ShouldBe(name);
        response.Errors.ShouldBeNull();
        response.StatusCode.ShouldBe(IResponse.Status.Success);
    }

    [Fact]
    public async Task InValid_name_empty_should_not_create_project()
    {
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39225");
        const string name = "";


        var response = await _sut.Handle(new ApiCreateProjectCommand(customerId: customerId, name: name), new CancellationToken());


        (await _context.Customers.FindAsync(customerId))?.Projects.Count.ShouldBe(2);

        response.ShouldBeOfType<ApiCreateProjectResponse>();
        response.Project.ShouldBeNull();
        response.Errors?.Count.ShouldBeGreaterThan(0);
        response.StatusCode.ShouldBe(IResponse.Status.Error);
    }

    [Fact]
    public async Task InValid_name_to_long_should_not_create_project()
    {
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39225");
        const string name = "asfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasfasf";


        var response = await _sut.Handle(new ApiCreateProjectCommand(customerId: customerId, name: name), new CancellationToken());


        (await _context.Customers.FindAsync(customerId))?.Projects.Count.ShouldBe(2);

        response.ShouldBeOfType<ApiCreateProjectResponse>();
        response.Project.ShouldBeNull();
        response.Errors?.Count.ShouldBeGreaterThan(0);
        response.StatusCode.ShouldBe(IResponse.Status.Error);
    }


    [Fact]
    public async Task InValid_customerId_should_not_create_project()
    {
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39290");
        const string name = "Testar";


        var response = await _sut.Handle(new ApiCreateProjectCommand(customerId: customerId, name: name), new CancellationToken());


        (await _context.Customers.FindAsync(customerId))?.Projects.Count.ShouldBe(2);

        response.ShouldBeOfType<ApiCreateProjectResponse>();
        response.Project.ShouldBeNull();
        response.Errors?.Count.ShouldBeGreaterThan(0);
        response.StatusCode.ShouldBe(IResponse.Status.NotFound);
    }
}
