namespace TaskManager.Contract.Requests;

public class CreateProjectRequest
{
    public required int Deadline { get; set; }
    public required string Title { get; set; }
}