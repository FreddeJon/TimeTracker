namespace Persistence;
public static class RegisterPersistenceServices
{
    public static void ConfigurePersistenceServices(this IServiceCollection services)
    {
        var sharedConfiguration = RegisterSharedSettings.GetSharedSettings();

        services.Configure<AccountOptions>(sharedConfiguration.GetSection(nameof(AccountOptions)));

        services.AddDbContext<TimeTrackerContext>(opt => opt.UseSqlServer(sharedConfiguration.GetConnectionString("DbConnection")));
    }
}
