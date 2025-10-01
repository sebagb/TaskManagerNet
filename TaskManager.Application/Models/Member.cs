namespace TaskManager.Application.Models;

public class Member
{
    public required Guid MemberId { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public bool IsAdmin { get; set; }
}