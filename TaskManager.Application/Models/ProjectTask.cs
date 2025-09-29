namespace TaskManager.Application.Models;

public class ProjectTask
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public required string Title { get; set; }
    public int Priority { get; set; } = 3;
}