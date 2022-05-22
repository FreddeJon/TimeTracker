namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource( "roles", "User Role (s)", new List<string>(){"role"})
        };
    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("TimeTrackerAPI", "TimeTracker API")
            {
                Scopes = { "admin_scope", "user_scope" }
            },

        };
    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {

            new ApiScope(name:"admin_scope", new List<string>(){"role"}),
            new ApiScope(name:"user_scope", new List<string>(){"role"}),

        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // Api
            new Client
            {

                ClientId = "TimeTrackerApi",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,

                AllowedScopes = { "user_scope", "admin_scope" },
            },

            // AdminClient
            new Client
            {
                ClientId = "AdminClient",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:5002/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                AlwaysIncludeUserClaimsInIdToken = true,

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "roles", "admin_scope" }
            },
            // ReactClient
            new Client
            {
                ClientId = "ReactClient",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                RequireClientSecret = false,

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "http://localhost:3000" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "http://localhost:3000" },

                AlwaysIncludeUserClaimsInIdToken = true,

                AllowedCorsOrigins = { "http://localhost:3000" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "roles", "user_scope", "admin_scope" }
            },
        };
}
