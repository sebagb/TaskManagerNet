namespace TaskManager.Application.Models;

public class Project
{
    public Guid Id { get; set; }
    public required int Deadline { get; set; }
    public string? Status { get; set; }
    public string? Tasks { get; set; }
    public required string Title { get; set; }
}