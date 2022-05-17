using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data;
public static class DataInitializer
{
    public static async Task InitializeData(this IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetService<TimeTrackerContext>();

        await context.Database.MigrateAsync();
    }
}
