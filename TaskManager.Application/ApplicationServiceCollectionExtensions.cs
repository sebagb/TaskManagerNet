using TaskManager.Application.Database;
using TaskManager.Application.Repositories;
using TaskManager.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Application;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddSingleton<IProjectRepository, ProjectRepository>();
        service.AddSingleton<IProjectService, ProjectService>();
        return service;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection service, string connectionString)
    {
        service.AddSingleton<IDbConnectionFactory>(_ =>
            new MySqlConnectionFactory(connectionString));
        service.AddSingleton<DbInitializer>();
        return service;
    }
}