namespace TaskManager.Contract.Responses;

public class MemberResponse
{
    public required Guid MemberId { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public bool IsAdmin { get; set; }
}