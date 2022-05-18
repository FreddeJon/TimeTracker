using Application.Features.API.Projects.Query.ApiGetProjects;

namespace UnitTests.Features.Api.Project.Query;
public class GetAllProjectsTest
{
    private readonly TimeTrackerContext _context;
    private readonly ApiGetProjectsForCustomerPaginatedQueryHandler _sut;

    public GetAllProjectsTest()
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
        _sut = new ApiGetProjectsForCustomerPaginatedQueryHandler(mapper, _context);


        _context.SeedCustomers();
    }



    [Fact]
    public async Task Valid_should_get_all_projects()
    {
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39225");
        var response = await _sut.Handle(new ApiGetProjectsForCustomerPaginatedQuery(customerId: customerId), new CancellationToken());

        response.ShouldBeOfType<ApiGetProjectsForCustomerPaginatedResponse>();
        response.Projects?.Count.ShouldBe(2);
        response.TotalCount.ShouldBe(2);
        response.StatusCode.ShouldBe(IResponse.Status.Success);
    }



    [Fact]
    public async Task Valid_offset_and_limit_should_get_all_projects()
    {
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39225");
        var response = await _sut.Handle(new ApiGetProjectsForCustomerPaginatedQuery(customerId: customerId, limit: 1), new CancellationToken());

        response.ShouldBeOfType<ApiGetProjectsForCustomerPaginatedResponse>();
        response.Projects?.Count.ShouldBe(1);
        response.TotalCount.ShouldBe(2);
        response.StatusCode.ShouldBe(IResponse.Status.Success);
    }


    [Fact]
    public async Task InValid_customerId_should_get_all_projects()
    {
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39290");
        var response = await _sut.Handle(new ApiGetProjectsForCustomerPaginatedQuery(customerId: customerId, limit: 1), new CancellationToken());

        response.ShouldBeOfType<ApiGetProjectsForCustomerPaginatedResponse>();
        response.Projects.ShouldBeNull();
        response.StatusCode.ShouldBe(IResponse.Status.NotFound);
        response.TotalCount.ShouldBe(0);
    }
}
