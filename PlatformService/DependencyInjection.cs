using System.Reflection;
using Mapster;
using PlatformService.Database;
using PlatformService.Database.Repositories;

namespace PlatformService;

public static class DependencyInjection
{
    public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
    {
        AddWebApiDependencies(services);
        AddDatabase(services);
        AddMapster(services);
        AddServices(services);

        return services;
    }

    private static void AddMapster(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }

    private static void AddDatabase(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>();
    }

    private static void AddWebApiDependencies(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddSingleton<PlatformRepository>();
    }
}
