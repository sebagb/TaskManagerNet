namespace TaskManager.Contract.Responses;

public class ProjectResponse
{
    public int Id { get; set; }
    public required int Deadline { get; set; }
    public string? Status { get; set; }
    public string? Tasks { get; set; }
    public required string Title { get; set; }
}