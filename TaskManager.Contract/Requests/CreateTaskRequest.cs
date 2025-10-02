namespace TaskManager.Contract.Requests;

public class CreateProjectTaskRequest
{
    public required string Title { get; set; }
    public int Priority { get; set; }
}