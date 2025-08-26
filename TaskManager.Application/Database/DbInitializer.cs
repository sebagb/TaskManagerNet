using System.Data;
using System.Data.Common;

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
        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Project (
            Id INTEGER PRIMARY KEY,
            Deadline INTEGER NOT NULL,
            Status VARCHAR(100),
            Tasks VARCHAR(100),
            Title VARCHAR(100) NOT NULL
            )";

        cmd.ExecuteNonQuery();
    }
}