namespace TaskManager.Contract.Requests;

public class GetTrustedMemberRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}