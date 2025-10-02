namespace TaskManager.Application.Models;

public class ProjectTask
{
    public required Guid Id { get; set; }
    public required Guid ProjectId { get; set; }
    public required string Title { get; set; }
    public int Priority { get; set; } = 3;
}