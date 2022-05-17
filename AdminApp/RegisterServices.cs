namespace AdminApp;
public static class RegisterServices
{
    internal static IServiceCollection ConfigureAdminApp(this IServiceCollection services)
    {

        // Get Shared settings
        var sharedConfiguration = RegisterSharedSettings.GetSharedSettings();

        // Register automapper profiles
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Register dependencies from other projects
        services.ConfigureApplicationServices();

        services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(opt => sharedConfiguration.Bind("AzureAdB2CAdminClient", opt));

        services.AddControllersWithViews(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        });
        services.AddRazorPages()
            .AddMicrosoftIdentityUI();


        return services;
    }
}
