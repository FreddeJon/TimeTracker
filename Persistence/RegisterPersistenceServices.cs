using Microsoft.Extensions.Configuration;
using Persistence.Data;

namespace Persistence;
public static class RegisterPersistenceServices
{
    public static void ConfigurePersistenceServices(this IServiceCollection services)
    {
        var sharedConfiguration = RegisterSharedSettings.GetSharedSettings();


        services.AddDbContext<TimeTrackerContext>(opt => opt.UseSqlServer(sharedConfiguration.GetConnectionString("DbConnection")));
    }
}
