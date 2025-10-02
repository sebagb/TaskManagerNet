namespace TaskManager.Contract.Responses;

public class ProjectTaskResponse
{
    public required Guid Id { get; set; }
    public required Guid ProjectId { get; set; }
    public required string Title { get; set; }
    public required int Priority { get; set; }
}