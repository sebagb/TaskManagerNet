using System.Data;

namespace TaskManager.Application.Database;

public interface IDbConnectionFactory
{
    public IDbConnection CreateConnection();
}