using Application.Features.API.Projects.Query.ApiGetProjectById;

namespace UnitTests.Features.Api.Project.Query;
public class GetProjectByIdTest
{
    private readonly ApiGetProjectByIdQueryHandler _sut;
    private readonly TimeTrackerContext _context;

    public GetProjectByIdTest()
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
        _sut = new ApiGetProjectByIdQueryHandler(mapper, _context);


        _context.SeedCustomers();
    }


    [Fact]
    public async Task Valid_should_get_project()
    {
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39225");
        var projectId = new Guid("4B2884EF-AFF2-4191-A186-EA5AA77A51FE");

        var response = await _sut.Handle(new ApiGetProjectByIdQuery(customerId: customerId, projectId: projectId), new CancellationToken());

        response.ShouldBeOfType<ApiGetProjectByIdResponse>();
        response.Project.ShouldNotBeNull();
        response.StatusCode.ShouldBe(IResponse.Status.Success);
    }
   
    [Fact]
    public async Task InValid_customerId_should_not_get_project()
    {
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39290");
        var projectId = new Guid("4B2884EF-AFF2-4191-A186-EA5AA77A51FE");

        var response = await _sut.Handle(new ApiGetProjectByIdQuery(customerId: customerId, projectId: projectId), new CancellationToken());

        response.ShouldBeOfType<ApiGetProjectByIdResponse>();
        response.Project.ShouldBeNull();
        response.StatusCode.ShouldBe(IResponse.Status.NotFound);
    }

    [Fact]
    public async Task InValid_projectId_should_not_get_project()
    {
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39225");
        var projectId = new Guid("4B2884EF-AFF2-4191-A186-EA5AA77A51DE");

        var response = await _sut.Handle(new ApiGetProjectByIdQuery(customerId: customerId, projectId: projectId), new CancellationToken());

        response.ShouldBeOfType<ApiGetProjectByIdResponse>();
        response.Project.ShouldBeNull();
        response.StatusCode.ShouldBe(IResponse.Status.NotFound);
    }
}

