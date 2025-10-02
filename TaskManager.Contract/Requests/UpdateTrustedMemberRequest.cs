namespace TaskManager.Contract.Requests;

public class UpdateTrustedMemberRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required bool IsAdmin { get; set; }
}