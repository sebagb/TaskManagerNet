namespace TaskManager.Contract.Requests;

public class CreateMemberRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}