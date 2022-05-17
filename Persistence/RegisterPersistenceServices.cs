using Microsoft.Extensions.Configuration;

namespace Persistence;
public static class RegisterPersistenceServices
{
    public static void ConfigurePersistenceServices(this IServiceCollection services)
    {
        var sharedConfiguration = RegisterSharedSettings.GetSharedSettings();


        services.AddDbContext<TimeTrackerContext>(opt => opt.UseSqlServer(sharedConfiguration.GetConnectionString("DbConnection")));
    }
}
