namespace Persistence.Data;

public static class DataInitializer
{
    public static async Task InitializeStartData(this IServiceProvider services)
    {
        var scope = services.CreateAsyncScope();
        try
        {
            var context = scope.ServiceProvider.GetService<TimeTrackerContext>();


            if (context is null)
            {
                throw new Exception("Problem with db");
            }




            await context.Database.MigrateAsync();

            await context.Customers.AddRangeAsync(GetData());

            await context.SaveChangesAsync();

        }
        finally
        {
            await scope.DisposeAsync();
        }
    }

    public static List<Customer> GetData()
    {
        var data = new List<Customer>()
        {
            new()
            {
                Name = "Klarna",
                Projects = new HashSet<Project>()
                {
                    new()
                    {
                        ProjectName = "Fix Databases",
                        TimeRegistrations = new HashSet<TimeRegistration>() { new()
                        {
                            Date = DateTime.Now.AddDays(-2),
                            Description = "Did some stuff",
                            TimeInMinutes = 60,
                        }}
                    },           
                    new()
                    {
                        ProjectName = "Payment",
                        TimeRegistrations = new HashSet<TimeRegistration>() { new()
                        {
                            Date = DateTime.Now.AddDays(-2),
                            Description = "Did some stuff",
                            TimeInMinutes = 60,
                        }}
                    }
                }
            },
            new()
            {
                Name = "Apple",
                Projects = new HashSet<Project>()
                {
                    new()
                    {
                        ProjectName = "Apple Pay",
                        TimeRegistrations = new HashSet<TimeRegistration>() { new()
                        {
                            Date = DateTime.Now.AddDays(-2),
                            Description = "Did some stuff",
                            TimeInMinutes = 80,
                        }}
                    }
                },

            },
            new()
            {
                Name = "Microsoft",
                Projects = new HashSet<Project>()
                {
                    new()
                    {
                        ProjectName = "Backend",
                        TimeRegistrations = new HashSet<TimeRegistration>() { new()
                        {
                            Date = DateTime.Now.AddDays(-2),
                            Description = "Did some stuff",
                            TimeInMinutes = 90,
                        }}
                    }
                },

            }
        };
        return data;
    }
}
