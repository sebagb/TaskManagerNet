using System.Data;
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
            VALUES ('{project.Id}',{project.Deadline},'{project.Status}','{project.Tasks}','{project.Title}')";

        return cmd.ExecuteNonQuery() > 0;
    }

    public Project? GetById(Guid id)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        var cmd = connection.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @$"SELECT * FROM Project WHERE Id = '{id}'";

        using var reader = cmd.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new Project()
        {
            Id = reader.GetGuid(0),
            Deadline = reader.GetInt32(1),
            Status = Enum.Parse<Project.State>(reader.GetString(2)),
            Tasks = reader.GetString(3),
            Title = reader.GetString(4)
        };
    }
}
