using Api;
using Application;
using Persistence;

namespace AdminClient;
public static class RegisterServices
{
    internal static IServiceCollection ConfigureAdminClient(this IServiceCollection services)
    {

        // Get Shared settings
        var sharedConfiguration = RegisterSharedSettings.GetSharedSettings();

        // Register automapper profiles
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Register dependencies from other projects
        services.ConfigureApplicationServices();


        //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
        //    .AddRoles<IdentityRole>()
        //    .AddEntityFrameworkStores<TimeTrackerContext>();

        services.AddControllersWithViews();

        return services;
    }
}
