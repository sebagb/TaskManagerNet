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
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @$"INSERT INTO Project
            (Id, Deadline, Status, Title)
            VALUES (
                '{project.Id}',
                {project.Deadline},
                '{project.Status}',
                '{project.Title}')";

        if (cmd.ExecuteNonQuery() < 1)
        {
            return false;
        }

        foreach (var task in project.Tasks)
        {
            cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @$"INSERT INTO ProjectTask
                (Id, ProjectId, Title, Priority)
                VALUES (
                    '{task.Id}',
                    '{task.ProjectId}',
                    '{task.Title}',
                    {task.Priority})";

            if (cmd.ExecuteNonQuery() < 1)
            {
                return false;
            }
        }

        return true;
    }

    public bool CreateProjectTask(ProjectTask task)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        var cmd = connection.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @$"INSERT INTO ProjectTask
                (Id, ProjectId, Title, Priority)
                VALUES (
                    '{task.Id}',
                    '{task.ProjectId}',
                    '{task.Title}',
                    {task.Priority})";

        return cmd.ExecuteNonQuery() > 0;
    }

    public bool DeleteById(Guid id)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        var cmd = connection.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @$"DELETE FROM ProjectTask WHERE ProjectId = '{id}'";

        cmd.ExecuteNonQuery();

        cmd = connection.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @$"DELETE FROM Project WHERE Id = '{id}'";

        return cmd.ExecuteNonQuery() > 0;
    }

    public bool DeleteProjectTaskById(Guid taskId)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        var cmd = connection.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @$"DELETE FROM ProjectTask WHERE Id = '{taskId}'";

        return cmd.ExecuteNonQuery() > 0;
    }

    public IEnumerable<Project> GetAll(GetAllProjectsOptions options)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        var projects = new List<Project>();

        var cmd = connection.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @$"SELECT *
            FROM Project
            LIMIT {options.PageSize}
            OFFSET {(options.Page - 1) * options.PageSize}";

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            projects.Add(new Project()
            {
                Id = reader.GetGuid(0),
                Deadline = reader.GetInt32(1),
                Status = Enum.Parse<Project.State>(reader.GetString(2)),
                Title = reader.GetString(3)
            });
        }

        reader.Close();

        if (projects.Count == 0)
        {
            return [];
        }

        foreach (var project in projects)
        {
            cmd.CommandText = @$"SELECT *
                FROM ProjectTask
                WHERE ProjectId = '{project.Id}'";

            using var taskReader = cmd.ExecuteReader();

            while (reader.Read())
            {
                project.Tasks.Add(new ProjectTask()
                {
                    Id = reader.GetGuid(0),
                    ProjectId = reader.GetGuid(1),
                    Title = reader.GetString(2),
                    Priority = reader.GetInt32(3)
                });
            }
        }


        return projects;
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

        var project = new Project()
        {
            Id = reader.GetGuid(0),
            Deadline = reader.GetInt32(1),
            Status = Enum.Parse<Project.State>(reader.GetString(2)),
            Title = reader.GetString(3)
        };
        reader.Close();

        cmd = connection.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @$"SELECT * FROM ProjectTask WHERE ProjectId = '{id}'";

        using var taskReader = cmd.ExecuteReader();

        while (reader.Read())
        {
            project.Tasks.Add(new ProjectTask()
            {
                Id = reader.GetGuid(0),
                ProjectId = reader.GetGuid(1),
                Title = reader.GetString(2),
                Priority = reader.GetInt32(3)
            });
        }

        return project;
    }

    public ProjectTask? GetProjectTaskById(Guid taskId)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        var cmd = connection.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @$"SELECT * FROM ProjectTask WHERE Id = '{taskId}'";

        using var reader = cmd.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new ProjectTask()
        {
            Id = reader.GetGuid(0),
            ProjectId = reader.GetGuid(1),
            Title = reader.GetString(2),
            Priority = reader.GetInt32(3)
        };
    }
}