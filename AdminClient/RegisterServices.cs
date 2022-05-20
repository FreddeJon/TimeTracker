using System.Security.Claims;
using Api;
using Application;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
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
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:5001";
                options.Audience = "IS4API";
            })
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "https://localhost:5001";

                options.ClientId = "AdminClient";
                options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
                options.ResponseType = "code";

                options.SaveTokens = true;

                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.GetClaimsFromUserInfoEndpoint = true;
                options.Scope.Add("roles");
                options.ClaimActions.MapUniqueJsonKey("role", "role");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role"
                };
            });


        services.AddAuthorization(opt =>
        {

            opt.AddPolicy("Admin", policy =>
                policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin"));
        });
        

        services.AddRazorPages();
        services.AddControllersWithViews(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            options.Filters.Add(new AuthorizeFilter(policy));

        });
        return services;
    }
}
