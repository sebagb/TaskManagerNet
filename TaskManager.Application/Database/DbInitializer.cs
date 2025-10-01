using System.Data;

namespace TaskManager.Application.Database;

public class DbInitializer
    (IDbConnectionFactory dbConnectionFactory)
{
    private readonly IDbConnectionFactory dbConnectionFactory = dbConnectionFactory;

    public void Initialize()
    {
        using var connection = dbConnectionFactory.CreateConnection();

        var cmd = connection.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Project (
            Id VARCHAR(255) PRIMARY KEY,
            Deadline INTEGER NOT NULL,
            Status VARCHAR(100),
            Title VARCHAR(100) NOT NULL
            );

            CREATE TABLE IF NOT EXISTS ProjectTask (
            Id VARCHAR(255) PRIMARY KEY,
            ProjectId VARCHAR(255),
            Title VARCHAR(100) NOT NULL,
            Priority INTEGER NOT NULL,
            FOREIGN KEY (ProjectId) REFERENCES Project(Id)
            );

            CREATE TABLE IF NOT EXISTS Member (
            Id VARCHAR(255) PRIMARY KEY,
            Username VARCHAR(255),
            Password VARCHAR(255),
            IsAdmin BOOL);";

        cmd.ExecuteNonQuery();
    }
}