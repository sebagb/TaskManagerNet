using TaskManager.Application.Database;
using TaskManager.Application.Repositories;
using TaskManager.Application.Services;

namespace TaskManager.Application;

public static class ApplicationServiceCollectionExtension
{
    public static IProjectService? Service { get; private set; }
    public static IProjectRepository? Repository { get; private set; }
    public static DbInitializer? Initializer { get; private set; }

    public static void AddApplication(string connectionString)
    {
        var mySqlConnectionFactory = new MySqlConnectionFactory(connectionString);
        Repository = new ProjectRepository(mySqlConnectionFactory);
        Initializer = new DbInitializer(mySqlConnectionFactory);
        Service = new ProjectService(Repository);
    }

}