namespace Persistence.Data;

public static class DataInitializer
{
    public static async Task InitializeStartData(this IServiceProvider services)
    {
        var scope = services.CreateAsyncScope();
        try
        {
            var context = scope.ServiceProvider.GetService<TimeTrackerContext>();
            var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            var accountOptions = scope.ServiceProvider.GetService<IOptions<AccountOptions>>()?.Value;

            if (context is null || userManager is null)
            {
                throw new Exception();
            }

            if (accountOptions?.AdminOptions is null || accountOptions.UserOptions is null)
            {
                throw new Exception();
            }


            await context.Database.MigrateAsync();

            await context.SeedRole(nameof(ApplicationRoles.Admin));
            await context.SeedRole(nameof(ApplicationRoles.User));


            await userManager.SeedIdentityUser(accountOptions.AdminOptions, new[] { nameof(ApplicationRoles.Admin), nameof(ApplicationRoles.User) });
            await userManager.SeedIdentityUser(accountOptions.UserOptions, new[] { nameof(ApplicationRoles.User) });
        }
        finally
        {
            await scope.DisposeAsync();
        }
    }


    private static async Task SeedRole(this TimeTrackerContext context, string role)
    {
        if (!await context.Roles.AnyAsync(x => x.Name == role))
        {
            await context.Roles.AddAsync(new IdentityRole { Name = role, NormalizedName = role });
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedIdentityUser(this UserManager<IdentityUser> userManager, BaseAccountOptions options, string[] roles)
    {
        if (await userManager.FindByEmailAsync(options.Email) is not null)
        {
            return;
        }

        var user = options.CreateIdentityUser();

        var result = await userManager.CreateAsync(user, options.Password);

        if (result.Succeeded)
        {
            await userManager.AddToRolesAsync(user, roles);
        }
    }

    private static IdentityUser CreateIdentityUser(this BaseAccountOptions options)
    {
        return new IdentityUser() { EmailConfirmed = true, Email = options.Email, UserName = options.Email };
    }
}
