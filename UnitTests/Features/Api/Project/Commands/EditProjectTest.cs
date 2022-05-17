using Application.Features.API.Projects.Commands.ApiEditProject;

namespace UnitTests.Features.Api.Project.Commands;

public class EditProjectTest
{
    private readonly TimeTrackerContext _context;
    private readonly ApiEditProjectCommandHandler _sut;

    public EditProjectTest()
    {
        var options = new DbContextOptionsBuilder<TimeTrackerContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<Profiles>();
        });

        var mapper = configurationProvider.CreateMapper();

        _context = new TimeTrackerContext(options);
        _sut = new ApiEditProjectCommandHandler(mapper, _context);


        _context.SeedCustomers();
    }


    [Fact]
    public async Task Valid_should_edit_project()
    {
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39225");
        var projectId = new Guid("4B2884EF-AFF2-4191-A186-EA5AA77A51FE");
        const string name = "Testar";


        var response =
            await _sut.Handle(new ApiEditProjectCommand(customerId, projectId, name), new CancellationToken());


        (await _context.Projects.FindAsync(projectId))?.ProjectName.ShouldBe(name);

        response.ShouldBeOfType<ApiEditProjectResponse>();
        response.Project?.ProjectName.ShouldBe(name);
        response.Error.ShouldBeNull();
        response.StatusCode.ShouldBe(IResponse.Status.Success);
    }


    [Fact]
    public async Task InValid_name_empty_should_not_edit_project()
    {
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39225");
        var projectId = new Guid("4B2884EF-AFF2-4191-A186-EA5AA77A51FE");
        const string name = "";


        var response =
            await _sut.Handle(new ApiEditProjectCommand(customerId, projectId, name), new CancellationToken());


        var proj = await _context.Projects.FindAsync(projectId);

        response.ShouldBeOfType<ApiEditProjectResponse>();
        response.Project.ShouldBeNull();
        response.Error?.Count.ShouldBeGreaterThan(0);
        response.StatusCode.ShouldBe(IResponse.Status.Error);
    }

    [Fact]
    public async Task InValid_name_to_long_should_not_edit_project()
    {
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39225");
        var projectId = new Guid("4B2884EF-AFF2-4191-A186-EA5AA77A51FE");
        const string name =
            "asfgasgasggaasfgasgasggaasfgasgasggaasfgasgasggaasfgasgasggaasfgasgasggaasfgasgasggaasfgasgasgga";


        var response =
            await _sut.Handle(new ApiEditProjectCommand(customerId, projectId, name), new CancellationToken());


        (await _context.Projects.FindAsync(projectId))?.ProjectName.ShouldBe("Testing Something");

        response.ShouldBeOfType<ApiEditProjectResponse>();
        response.Project.ShouldBeNull();
        response.Error?.Count.ShouldBeGreaterThan(0);
        response.StatusCode.ShouldBe(IResponse.Status.Error);
    }


    [Fact]
    public async Task InValid_projectId_should_not_edit_project()
    {
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39225");
        var projectId = new Guid("4B2884EF-AFF2-4191-A186-EA5AA77A51DE");
        const string name = "Testar";


        var response =
            await _sut.Handle(new ApiEditProjectCommand(customerId, projectId, name), new CancellationToken());


        (await _context.Projects.FindAsync(projectId)).ShouldBeNull();

        response.ShouldBeOfType<ApiEditProjectResponse>();
        response.Project.ShouldBeNull();
        response.Error?.Count.ShouldBeGreaterThan(0);
        response.StatusCode.ShouldBe(IResponse.Status.NotFound);
    }

    [Fact]
    public async Task InValid_customerId_should_not_edit_project()
    {
        var customerId = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39290");
        var projectId = new Guid("4B2884EF-AFF2-4191-A186-EA5AA77A51DE");
        const string name = "Testar";


        var response =
            await _sut.Handle(new ApiEditProjectCommand(customerId, projectId, name), new CancellationToken());


        (await _context.Projects.FindAsync(projectId)).ShouldBeNull();

        response.ShouldBeOfType<ApiEditProjectResponse>();
        response.Project.ShouldBeNull();
        response.Error?.Count.ShouldBeGreaterThan(0);
        response.StatusCode.ShouldBe(IResponse.Status.NotFound);
    }
}
