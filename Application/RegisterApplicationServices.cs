using System.Reflection;

namespace Application;
public static class RegisterApplicationServices
{
    public static void ConfigureApplicationServices(this IServiceCollection services)
    {
        services.ConfigurePersistenceServices();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}
