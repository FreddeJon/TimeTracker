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


        }
        finally
        {
            await scope.DisposeAsync();
        }
    }

}
