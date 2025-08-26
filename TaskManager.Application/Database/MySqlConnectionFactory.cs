using System.Data;
using MySqlConnector;

namespace TaskManager.Application.Database;

public class MySqlConnectionFactory
    (string connectionString)
    : IDbConnectionFactory
{
    private readonly string connectionString = connectionString;

    public IDbConnection CreateConnection()
    {
        var connection = new MySqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}
