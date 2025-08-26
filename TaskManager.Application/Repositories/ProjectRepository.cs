using TaskManager.Application.Database;
using TaskManager.Application.Models;

namespace TaskManager.Application.Repositories;

public class ProjectRepository
    (IDbConnectionFactory dbConnectionFactory)
    : IProjectRepository
{
    public bool CreateProject(Project project)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        var cmd = connection.CreateCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = @$"INSERT INTO Project
            (Id, Deadline, Status, Tasks, Title)
            VALUES ({project.Id},{project.Deadline},'{project.Status}','{project.Tasks}','{project.Title}')";

        return cmd.ExecuteNonQuery() > 0;
    }
}
