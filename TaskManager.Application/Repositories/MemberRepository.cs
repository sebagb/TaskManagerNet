using System.Data;
using TaskManager.Application.Database;
using TaskManager.Application.Models;

namespace TaskManager.Application.Repositories;

public class MemberRepository
    (IDbConnectionFactory dbConnectionFactory) : IMemberRepository
{

    public bool CreateMember(Member member)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        var cmd = connection.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @$"INSERT INTO Member
            (Id, Username, Password, IsAdmin)
            VALUES (
                '{member.MemberId}',
                '{member.Username}',
                '{member.Password}',
                {member.IsAdmin})";

        return cmd.ExecuteNonQuery() > 0;
    }

    public Member? GetByCredentials(string username, string password)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        var cmd = connection.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @$"SELECT * FROM Member
            WHERE Username = '{username}'
            AND Password =  '{password}'";

        using var reader = cmd.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new Member()
        {
            MemberId = reader.GetGuid(0),
            Username = reader.GetString(1),
            Password = reader.GetString(2),
            IsAdmin = reader.GetBoolean(3)
        };
    }

    public bool Update(Member member)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        var cmd = connection.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @$"UPDATE Member
            SET Username = '{member.Username}',
            Password =  '{member.Password}',
            IsAdmin = {member.IsAdmin}
            WHERE Id = '{member.MemberId}'";

        return cmd.ExecuteNonQuery() > 0;
    }
}